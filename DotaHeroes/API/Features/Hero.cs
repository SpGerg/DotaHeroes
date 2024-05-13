using CustomPlayerEffects;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Events.EventArgs.Hero;
using DotaHeroes.API.Features.Components;
using DotaHeroes.API.Features.Objects;
using DotaHeroes.API.Interfaces;
using DotaHeroes.API.Statistics;
using Exiled.API.Features;
using MEC;
using NorthwoodLib.Pools;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DotaHeroes.API.Features
{
    public abstract class Hero
    {
        public abstract string HeroName { get; }

        public abstract string Slug { get; }

        public abstract HeroClassType HeroClassType { get; set; }

        public virtual List<Ability> Abilities
        {
            get
            {
                return abilities;
            }
            set
            {
                abilities = value;

                if (abilities == null)
                {
                    abilities = new List<Ability>();
                }
            }
        }

        public virtual RoleTypeId Model => RoleTypeId.Tutorial;

        public virtual List<RoleTypeId> ChangeRoles { get; set; }

        public Player Player { get; private set; }

        public HeroStatistics HeroStatistics { get; protected set; } = new HeroStatistics();

        public Inventory Inventory { get; protected set; }

        public HeroStateType HeroStateType
        {
            get
            {
                return heroStateType;
            }
            set
            {
                if (Player == null) return;

                heroStateType = value;

                if (value == HeroStateType.Casting)
                {
                    Player.EnableEffect<Ensnared>();
                }
                else
                {
                    Player.DisableEffect<Ensnared>();
                }
            }
        }

        public SideType SideType { get; set; } = SideType.None;

        public float ProjectileSpeed { get; set; } = 0.6f;

        public List<ProjectileObject> ProjectilesFollow { get; }

        public List<Aura> OwnedAuras { get; }

        public HeroController HeroController { get; private set; }

        public int Level { get; set; } = 1;

        public int Experience
        {
            get
            {
                return experience;
            }
            set
            {
                experience = value;

                if (experience >= Constants.ExperienceToLevelUp[Level])
                {
                    var left = experience - Constants.ExperienceToLevelUp[Level];

                    LevelUp();

                    experience = Constants.ExperienceToLevelUp[Level] + left;
                }
            }
        }

        public int PointsToLevelUp { get; private set; } = 1;

        public bool IsHeroDead
        {
            get
            {
                return isHeroDead;
            }
            private set
            {
                isHeroDead = value;
            }
        }

        public Dictionary<string, object> Values { get; }

        public bool IsHealthRegeneration { get; set; }

        public bool IsManaRegeneration { get; set; }

        public float Money { get; set; }

        protected List<Effect> Effects { get; set; }

        private List<Ability> abilities;

        private Ability lastAbilityLevelUpped;

        private HeroStateType heroStateType;

        private bool isHeroDead;

        private int experience;

        static Hero()
        {
            Timing.RunCoroutine(RegenerationCoroutine());
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Hero" /> class.
        /// </summary>
        public Hero()
        {
            Player = null;
            Abilities = new List<Ability>();
            Effects = new List<Effect>();
            OwnedAuras = new List<Aura>();
            Values = new Dictionary<string, object>();
            Inventory = new Inventory(this);

            if (ChangeRoles == null)
            {
                ChangeRoles = new List<RoleTypeId>();
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Hero" /> class.
        /// </summary>
        /// <param name="player"><inheritdoc cref="Player" /></param>
        /// <param name="sideType"><inheritdoc cref="SideType" /></param>
        protected Hero(Player player, SideType sideType) : this()
        {
            Player = player;
            SideType = sideType;

            Money = 1200;

            if (Plugin.Instance.Config.Debug)
            {
                Money = short.MaxValue;
                PointsToLevelUp = short.MaxValue;
            }

            if (Player != null)
            {
                DTAPI.SetOrAddPlayer(player.Id, this);
            }

            HeroController = Player.GameObject.GetComponent<HeroController>();
        }

        /// <summary>
        /// Level up.
        /// </summary>
        public void LevelUp()
        {
            if (Level >= 30) return;

            HeroStatistics.LevelUp();
            Level++;

            if (Level > 1)
            {
                Log.Info($"Player {Player.Nickname} hero {HeroName} is level up from {Level - 1} to {Level}");
            }
        }

        /// <summary>
        /// Level up.
        /// </summary>
        public bool LevelUpAbilty(string slug)
        {
            if (Level >= 30 || PointsToLevelUp == 0 || (lastAbilityLevelUpped != null && lastAbilityLevelUpped.Slug == Slug)) return false;

            var ability = Abilities.FirstOrDefault(ability => ability.Slug == slug);

            if (ability == default) return false;

            ability.LevelUp();

            lastAbilityLevelUpped = ability;
            PointsToLevelUp--;

            Log.Info($"Player {Player.Nickname} hero {HeroName} is level up ability {ability.Name} from {ability.Level - 1} to {ability.Level}");

            return true;
        }

        /// <summary>
        /// Level up.
        /// </summary>
        public void LevelUpAbilty<T>() where T : Ability, new()
        {
            LevelUpAbilty(new T().Name);
        }

        /// <summary>
        /// Execute ability by name.
        /// </summary>
        public bool ExecuteAbility(Ability ability, ref string response, bool ignoreAvailability = false)
        {
            if (HeroStateType != HeroStateType.None) return false;

            if (!ignoreAvailability)
            {
                ability = Abilities.First(_ability => _ability.Slug == ability.Slug);
            }

            if (ability == default)
            {
                return false;
            }

            Hud.Update();

            if (ability is not PassiveAbility)
            {
                var executingAbility = new HeroExecutingAbilityEventArgs(this, ability, true);
                Events.Handlers.Hero.OnHeroExecutingAbility(executingAbility);

                if (!executingAbility.IsAllowed) return false;

                ability = executingAbility.Ability;

                var result = false;

                if (ability is ActiveAbility activeAbility)
                {
                    result = activeAbility.CheckAndExecute(Utils.EmptyArraySegment, out response);
                }

                if (ability is ToggleAbility toggleAbility)
                {
                    result = toggleAbility.CheckAndExecute(Utils.EmptyArraySegment, out response);
                }

                if (result && abilities is ICost cost)
                {
                    HeroStatistics.HealthAndMana.Health -= cost.HealthCost;
                    HeroStatistics.HealthAndMana.Mana -= cost.ManaCost;
                }

                var executedAbility = new HeroExecutedAbilityEventArgs(this, ability);
                Events.Handlers.Hero.OnHeroExecutedAbility(executedAbility);
            }

            return true;
        }

        /// <summary>
        /// Execute ability by type.
        /// </summary>
        public bool ExecuteAbility<T>(ref string response) where T : Ability, new()
        {
            ExecuteAbility(new T().Create(this), ref response);

            return true;
        }

        /// <summary>
        /// Take damage
        /// </summary>
        public virtual double TakeDamage(Hero attacker, double damage, DamageType damageType, bool isCheckDead = true)
        {
            Log.Debug("First damage " + damage + " by " + (attacker == null ? "attacker is null lol" : attacker.Player.Nickname));

            var takingDamage = new HeroTakingDamageEventArgs(this, attacker, damage, damageType, true);
            Events.Handlers.Hero.OnHeroTakingDamage(takingDamage);

            if (!takingDamage.IsAllowed) return -1;

            double _damage = takingDamage.Damage;
            Log.Debug("Second Damage " + _damage + " by " + (attacker == null ? "attacker is null lol" : attacker.Player.Nickname));
            double total_damage = 0;

            switch (damageType)
            {
                case DamageType.None: break;
                case DamageType.Physical:
                    total_damage = HeroStatistics.Armor.GetPhysicalDamage(_damage, HeroStatistics.Agility);
                    break;
                case DamageType.Magical:
                    total_damage = HeroStatistics.Resistance.GetMagicalDamage(_damage, HeroStatistics.Intelligence);

                    break;
                case DamageType.Pure:
                    total_damage = _damage;
                    break;
                default:
                    total_damage = _damage;
                    break;
            }

            Log.Debug("Third Damage " + total_damage + " by " + (attacker == null ? "attacker is null lol" : attacker.Player.Nickname));

            ReduceHealthAndCheckForDead(attacker, total_damage, isCheckDead);

            var takedDamage = new HeroTakedDamageEventArgs(this, attacker, total_damage, damageType);
            Events.Handlers.Hero.OnHeroTakedDamage(takedDamage);

            return total_damage;
        }

        /// <summary>
        /// Take damage without attacker
        /// </summary>
        public virtual double TakeDamage(double damage, DamageType damageType, bool isCheckDead = true)
        {
            var result = TakeDamage(null, damage, damageType, isCheckDead);

            return result;
        }

        /// <summary>
        /// Heal
        /// </summary>
        public virtual void Heal(double heal, Hero hero)
        {
            var healing = new HeroHealingEventArgs(this, hero, heal, true);
            Events.Handlers.Hero.OnHeroHealing(healing);

            if (!healing.IsAllowed) return;

            Log.Debug("Healed " + heal);

            HeroStatistics.HealthAndMana.Health += healing.Heal;

            var healed = new HeroHealedEventArgs(this, hero, healing.Heal);
            Events.Handlers.Hero.OnHeroHealed(healed);
        }

        /// <summary>
        /// Heal without player
        /// </summary>
        public virtual void Heal(int heal)
        {
            Heal(heal, null);
        }

        /// <summary>
        /// Attack to target
        /// </summary>
        public virtual void Attack(Hero target)
        {
            if (HeroStateType != HeroStateType.None) return;

            var attacking = new HeroAttackingEventArgs(this, target, HeroStatistics.Attack.FullDamage, DamageType.Physical, true);
            Events.Handlers.Hero.OnHeroAttacking(attacking);

            if (!attacking.IsAllowed) return;

            target.TakeDamage(this, attacking.Damage, attacking.DamageType);

            var attacked = new HeroAttackedEventArgs(this, target, HeroStatistics.Attack.FullDamage, DamageType.Physical);
            Events.Handlers.Hero.OnHeroAttacked(attacked);
        }

        /// <summary>
        /// Dead
        /// </summary>
        public virtual void Dead(Hero killer = null)
        {
            var dying = new HeroDyingEventArgs(this, killer, true);
            Events.Handlers.Hero.OnHeroDying(dying);

            if (!dying.IsAllowed) return;

            Hud.Clear(Player);

            ApplyDispel(DispelType.Dead);

            Player.Role.Set(RoleTypeId.Spectator);

            foreach (var ability in Abilities)
            {
                if (ability is ToggleAbility toggleAbility)
                {
                    toggleAbility.IsActive = false;
                    toggleAbility.Deactivate(new ArraySegment<string>(), out string response);
                }
            }

            foreach (var effect in Effects)
            {
                DisableEffect(effect);
            }

            IsHealthRegeneration = false;
            IsManaRegeneration = false;

            IsHeroDead = true;

            var died = new HeroDiedEventArgs(this, killer);
            Events.Handlers.Hero.OnHeroDied(died);
        }

        /// <summary>
        /// Apply dispel.
        /// </summary>
        public void ApplyDispel(DispelType dispelType, Hero dispeller)
        {
            if (dispelType == DispelType.NotDispelling)
            {
                return;
            }

            var dispelledEffects = new List<Effect>();

            if (dispelType == DispelType.Dead)
            {
                foreach (var effect in Effects)
                {
                    if (effect.DispelType != DispelType.None)
                    {
                        dispelledEffects.Add(effect);
                    }
                }

                return;
            }

            if (dispelType == DispelType.Strong)
            {
                foreach (var effect in Effects)
                {
                    if (effect.DispelType == DispelType.None)
                    {
                        continue;
                    }

                    if (effect.DispelType == DispelType.Basic || effect.DispelType == DispelType.Strong)
                    {
                        dispelledEffects.Add(effect);
                    }
                }

                return;
            }

            if (dispelType == DispelType.Basic)
            {
                foreach (var effect in Effects)
                {
                    if (effect.DispelType == DispelType.None)
                    {
                        continue;
                    }

                    if (effect.DispelType == DispelType.Basic)
                    {
                        dispelledEffects.Add(effect);
                    }
                }
                return;
            }

            var dispelling = new HeroDispellingEventArgs(this, dispeller, dispelledEffects, dispelType, true);
            Events.Handlers.Hero.OnHeroDispelling(dispelling);

            if (!dispelling.IsAllowed) return;

            foreach (var effect in dispelling.EffectsToDispel)
            {
                effect.Dispelled();
                Effects.Remove(effect);
            }
            Hud.Update();

            var dispelled = new HeroDispelledEventArgs(this, dispeller, dispelledEffects, dispelType);
            Events.Handlers.Hero.OnHeroDispelled(dispelled);
        }

        /// <summary>
        /// Apply dispel without dispeller.
        /// </summary>
        public void ApplyDispel(DispelType dispelType)
        {
            ApplyDispel(dispelType, null);
        }

        /// <summary>
        /// Enable effect by copy.
        /// </summary>
        public Effect EnableEffect(Effect _effect, float duration = 3f)
        {
            if (!_effect.IsStacking && TryGetEffect(_effect, out Effect result))
            {
                return result;
            }

            var receivingEffect = new HeroReceivingEffectEventArgs(this, _effect, true);
            Events.Handlers.Hero.OnHeroReceivingEffect(receivingEffect);

            if (!receivingEffect.IsAllowed) return null;

            if (receivingEffect.Effect is IEffectDuration _duration)
            {
                _duration.Duration = duration;
            }

            Utils.AddModifier(this, receivingEffect.Effect as IModifier);

            receivingEffect.Effect.Enabled();
            Effects.Add(receivingEffect.Effect);

            Hud.Update(this);

            return receivingEffect.Effect;
        }

        /// <summary>
        /// Execute effect by type.
        /// </summary>
        public void ExecuteEffect<T>() where T : Effect, new()
        {
            if (!TryGetEffect(out T result))
            {
                return;
            }

            Hud.Update();

            result.Executed();
        }

        /// <summary>
        /// Execute effect by copy.
        /// </summary>
        public void ExecuteEffect(Effect effect)
        {
            if (!TryGetEffect(effect, out Effect result))
            {
                return;
            }

            Hud.Update();

            result.Executed();
        }

        /// <summary>
        /// Disable effect by type.
        /// </summary>
        public void DisableEffect<T>() where T : Effect, new()
        {
            if (!TryGetEffect(out T result))
            {
                return;
            }

            result.Disabled();
            Effects.Remove(result);

            Utils.RemoveModifier(this, result as IModifier);
            Hud.Update();

            var disabledEffect = new HeroDisabledEffectEventArgs(this, result);
            Events.Handlers.Hero.OnHeroDisabledEffect(disabledEffect);
        }

        /// <summary>
        /// Disable effect by copy.
        /// </summary>
        public void DisableEffect(Effect effect)
        {
            if (!TryGetEffect(effect, out Effect result))
            {
                return;
            }

            result.Disabled();
            Effects.Remove(result);

            Utils.RemoveModifier(this, result as IModifier);
            Hud.Update();

            var disabledEffect = new HeroDisabledEffectEventArgs(this, effect);
            Events.Handlers.Hero.OnHeroDisabledEffect(disabledEffect);
        }

        /// <summary>
        /// Get effect or default by type.
        /// </summary>
        public T GetEffectOrDefault<T>() where T : Effect, new()
        {
            return (T)Effects.Find(_effect => _effect is T);
        }

        /// <summary>
        /// Get effect or default by copy.
        /// </summary>
        public Effect GetEffectOrDefault(Effect effect)
        {
            return Effects.Find(_effect => _effect.Name == effect.Name);
        }

        /// <summary>
        /// Try get effect or default by type.
        /// </summary>
        public bool TryGetEffect<T>(out T result) where T : Effect, new()
        {
            var effect = GetEffectOrDefault<T>();
            result = effect;

            if (effect == default)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Try get effect or default by copy.
        /// </summary>
        public bool TryGetEffect(Effect _effect, out Effect result)
        {
            var effect = GetEffectOrDefault(_effect);
            result = effect;

            if (effect == default)
            {
                return false;
            }

            return true;
        }

        public List<Effect> GetEffects()
        {
            return new List<Effect>(Effects);
        }

        /// <summary>
        /// Respawn
        /// </summary>
        public virtual void Respawn()
        {
            var healthAndMana = HeroStatistics.HealthAndMana;
            healthAndMana.Health = healthAndMana.MaximumHealth;
            healthAndMana.Mana = healthAndMana.MaximumMana;
            IsHealthRegeneration = true;
            IsManaRegeneration = true;
            IsHeroDead = false;
        }

        /// <summary>
        /// Clearing projectiles
        /// </summary>
        public virtual void ClearProjectiles()
        {
            foreach (var projectile in ProjectilesFollow)
            {
                if (projectile.IsIgnoreClear) continue;

                projectile.Target = null;
            }
        }

        /// <summary>
        /// To string
        /// </summary>
        public override string ToString()
        {
            var stringBuilder = StringBuilderPool.Shared.Rent();

            stringBuilder.AppendLine("Name: " + HeroName);
            stringBuilder.AppendLine("Level: " + Level);
            stringBuilder.AppendLine("Money: " + Money);
            stringBuilder.AppendLine("Hero statistics: " + HeroStatistics.ToString());

            if (Effects.Count > 0)
            {
                stringBuilder.AppendLine(new string('—', 16));
                stringBuilder.AppendLine("Effects: ");

                foreach (var effect in Effects)
                {
                    if (effect.IsVisible)
                    {
                        stringBuilder.AppendLine(effect.ToString());

                        stringBuilder.AppendLine($"— {effect.Description}");

                        if (effect.Stack > -1)
                        {
                            stringBuilder.AppendLine(effect.Stack.ToString());
                        }
                    }
                }
            }

            return StringBuilderPool.Shared.ToStringReturn(stringBuilder);
        }

        public abstract Hero Create(Player player, SideType sideType);

        private static IEnumerator<float> RegenerationCoroutine()
        {
            while (true)
            {
                foreach (var hero in DTAPI.GetHeroes().Values)
                {
                    var healthAndMana = hero.HeroStatistics.HealthAndMana;

                    if (hero.IsHealthRegeneration)
                    {
                        healthAndMana.Health += healthAndMana.HealthRegeneration;
                    }

                    if (hero.IsManaRegeneration)
                    {
                        healthAndMana.Mana += healthAndMana.ManaRegeneration;
                    }

                    healthAndMana.Health = (double)Mathf.Clamp((float)healthAndMana.Health, 0, (float)healthAndMana.MaximumHealth);
                    healthAndMana.Mana = (double)Mathf.Clamp((float)healthAndMana.Mana, 0, (float)healthAndMana.MaximumMana);

                    Hud.Update(hero);
                }

                yield return Timing.WaitForSeconds(1);
            }
        }

        private void ReduceHealthAndCheckForDead(Hero killer, double damage, bool isCheck = true)
        {
            HeroStatistics.HealthAndMana.Health -= damage;

            if (HeroStatistics.HealthAndMana.Health < 0)
            {
                if (isCheck)
                {
                    Dead(killer);
                }
                else
                {
                    HeroStatistics.HealthAndMana.Health = 1;
                }
            }
        }
    }
}

