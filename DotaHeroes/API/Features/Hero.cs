using CustomPlayerEffects;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Events.EventArgs.Hero;
using DotaHeroes.API.Events.Handlers;
using DotaHeroes.API.Features.Components;
using DotaHeroes.API.Features.Objects;
using DotaHeroes.API.Heroes;
using DotaHeroes.API.Interfaces;
using DotaHeroes.API.Statistics;
using Exiled.API.Features;
using Exiled.API.Features.Roles;
using Exiled.CustomRoles.API.Features;
using NorthwoodLib.Pools;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityStandardAssets.CrossPlatformInput;

namespace DotaHeroes.API.Features
{
    public abstract class Hero : IHeroFactory
    {
        public abstract string HeroName { get; }

        public abstract HeroClassType HeroClassType { get; set; }

        public virtual List<Ability> Abilities => new List<Ability>();

        public virtual RoleTypeId Model => RoleTypeId.Tutorial;

        public Player Player { get; private set; }

        public HeroStatistics HeroStatistics { get; protected set; }

        public HeroStateType HeroStateType {
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

        public HeroController HeroController { get; private set; }

        public int Level { get; set; }

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

        private List<Effect> Effects { get; }

        private HeroStateType heroStateType;

        private bool isHeroDead;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Hero" /> class.
        /// </summary>
        public Hero()
        {
            Player = null;
            Effects = new List<Effect>();
            Values = new Dictionary<string, object>();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Hero" /> class.
        /// </summary>
        /// <param name="player"><inheritdoc cref="Player" /></param>
        /// <param name="sideType"><inheritdoc cref="SideType" /></param>
        public Hero(Player player, SideType sideType)
        {
            Player = player;
            SideType = sideType;

            Effects = new List<Effect>();
            Values = new Dictionary<string, object>();

            API.SetOrAddPlayer(player.Id, this);

            HeroController = Player.GameObject.GetComponent<HeroController>();

            foreach (var ability in Abilities)
            {
                if (ability is IValues)
                {
                    var values = (ability as IValues).Values;
                    if (values.ContainsKey("cooldown"))
                    {
                        Cooldowns.AddCooldown(player.Id, new CooldownInfo(ability.Name, (int)values["cooldown"][ability.Level]));
                    }
                }

                if (ability is IPassiveAbility)
                {
                    (ability as IPassiveAbility).Register(this);
                }
            }
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
                effect.Dispel();
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
        /// Execute ability by name.
        /// </summary>
        public bool ExecuteAbility(string name)
        {
            if (HeroStateType != HeroStateType.None) return false;

            var ability = Abilities.First(ability => ability.Name == name);

            if (ability == default)
            {
                return false;
            }

            Hud.Update();

            if (ability is ActiveAbility)
            {
                (ability as ActiveAbility).Execute(this, new ArraySegment<string>(), out string response);
            }

            if (ability is ToggleAbility)
            {
                (ability as ToggleAbility).Execute(this, new ArraySegment<string>(), out string response);
            }

            return true;
        }

        /// <summary>
        /// Execute ability by type.
        /// </summary>
        public bool ExecuteAbility<T>() where T : Ability, new()
        {
            if (HeroStateType != HeroStateType.None) return false;

            var ability = new T();

            Hud.Update();

            if (Abilities.FirstOrDefault(_ability => _ability == ability) == default)
            {
                return false;
            }

            if (ability is ActiveAbility)
            {
                (ability as ActiveAbility).Execute(this, new ArraySegment<string>(), out string response);
            }

            if (ability is ToggleAbility)
            {
                (ability as ToggleAbility).Execute(this, new ArraySegment<string>(), out string response);
            }

            return true;
        }

        /// <summary>
        /// Enable effect by type.
        /// </summary>
        public T EnableEffect<T>() where T : Effect, new()
        {
            Hud.Update();

            return (T)EnableEffect(new T());
        }

        /// <summary>
        /// Enable effect by copy.
        /// </summary>
        public Effect EnableEffect(Effect _effect)
        {
            if (TryGetEffect(_effect, out Effect result))
            {
                return result;
            }

            Hud.Update();

            var receivingEffect = new HeroReceivingEffectEventArgs(this, _effect, true);
            Events.Handlers.Hero.OnHeroReceivingEffect(receivingEffect);

            if (!receivingEffect.IsAllowed) return null;

            receivingEffect.Effect.Enable();
            Effects.Add(receivingEffect.Effect);

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

            result.Execute();
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

            result.Execute();
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

            result.Disable();
            Effects.Remove(result);

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

            result.Disable();
            Effects.Remove(result);

            Hud.Update();

            var disabledEffect = new HeroDisabledEffectEventArgs(this, effect);
            Events.Handlers.Hero.OnHeroDisabledEffect(disabledEffect);
        }

        /// <summary>
        /// Get effect or default by type.
        /// </summary>
        public T GetEffectOrDefault<T>() where T : Effect, new()
        {
            return (T)Effects.FirstOrDefault(_effect => _effect is T);
        }

        /// <summary>
        /// Get effect or default by copy.
        /// </summary>
        public Effect GetEffectOrDefault(Effect effect)
        {
            return Effects.FirstOrDefault(_effect => _effect.Name == effect.Name);
        }

        /// <summary>
        ///     Get effects (new copy).
        /// </summary>
        public List<Effect> GetEffects()
        {
            return new List<Effect>(Effects);
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

        /// <summary>
        /// Take damage without attacker
        /// </summary>
        public virtual decimal TakeDamage(int damage, DamageType damageType)
        {
            var takingDamage = new HeroTakingDamageEventArgs(this, null, damage, damageType, true);
            Events.Handlers.Hero.OnHeroTakingDamage(takingDamage);

            if (!takingDamage.IsAllowed) return -1;

            int _damage = takingDamage.Damage;
            decimal total_damage = 0;

            switch (damageType)
            {
                case DamageType.None: break;
                case DamageType.Physical:
                    decimal armor = (decimal)(HeroStatistics.Armor.BaseArmor + HeroStatistics.Armor.ExtraArmor);
                    decimal armor_percent = (0.052m * armor) / (0.9m + 0.048m * armor);
                    total_damage = (int)(_damage - ((_damage / 100) * armor_percent));

                    break;
                case DamageType.Magical:
                    decimal percent = (_damage / 100m);
                    total_damage = _damage - percent * (decimal)HeroStatistics.Resistance.GetMagicResistance(HeroStatistics.Intelligence);

                    break;
                case DamageType.Pure:
                    total_damage = _damage;
                    break;
                default:
                    total_damage = _damage;
                    break;
            }

            ReduceHealthAndCheckForDead((float)total_damage);

            var takedDamage = new HeroTakedDamageEventArgs(this, null, _damage, damageType);
            Events.Handlers.Hero.OnHeroTakedDamage(takedDamage);

            return total_damage;
        }

        /// <summary>
        /// Take damage
        /// </summary>
        public virtual decimal TakeDamage(Hero attacker, int damage, DamageType damageType)
        {
            var takingDamage = new HeroTakingDamageEventArgs(this, attacker, damage, damageType, true);
            Events.Handlers.Hero.OnHeroTakingDamage(takingDamage);

            var result = TakeDamage(damage, damageType);

            var takedDamage = new HeroTakedDamageEventArgs(this, attacker, damage, damageType);
            Events.Handlers.Hero.OnHeroTakedDamage(takedDamage);

            return result;
        }


        private void ReduceHealthAndCheckForDead(float damage)
        {
            HeroStatistics.HealthAndMana.Health -= damage;

            if (HeroStatistics.HealthAndMana.Health < 0)
            {
                Dead();
            }
        }

        /// <summary>
        /// Heal
        /// </summary>
        public virtual void Heal(int heal, Hero hero)
        {
            var healing = new HeroHealingEventArgs(this, hero, heal, true);
            Events.Handlers.Hero.OnHeroHealing(healing);

            if (!healing.IsAllowed) return;

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

            target.TakeDamage(attacking.Damage, attacking.DamageType);

            var attacked = new HeroAttackedEventArgs(this, target, HeroStatistics.Attack.FullDamage, DamageType.Physical);
            Events.Handlers.Hero.OnHeroAttacked(attacked);
        }

        /// <summary>
        /// Dead
        /// </summary>
        public virtual void Dead()
        {
            var dying = new HeroDyingEventArgs(this, true);
            Events.Handlers.Hero.OnHeroDying(dying);

            if (!dying.IsAllowed) return;

            ApplyDispel(DispelType.Dead);

            Player.Role.Set(RoleTypeId.Spectator);

            foreach (var ability in Abilities)
            {
                if (ability is ToggleAbility)
                {
                    var toggle = ability as ToggleAbility;

                    toggle.IsActive = false;
                    toggle.Deactivate(this, new ArraySegment<string>(), out string response);
                }
            }

            foreach (var effect in Effects)
            {
                DisableEffect(effect);
            }

            var died = new HeroDiedEventArgs(this);
            Events.Handlers.Hero.OnHeroDied(died);

            IsHeroDead = true;
        }

        /// <summary>
        /// Respawn
        /// </summary>
        public virtual void Respawn() 
        {
            var healthAndMana = HeroStatistics.HealthAndMana;
            healthAndMana.Health = healthAndMana.MaximumHealth;
            healthAndMana.Mana = healthAndMana.MaximumMana;
            IsHeroDead = false;
        }

        /// <summary>
        /// To string
        /// </summary>
        public override string ToString()
        {
            var stringBuilder = StringBuilderPool.Shared.Rent();

            stringBuilder.AppendLine("Hero: ");
            stringBuilder.AppendLine("Name: " + HeroName);
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
                    }
                }
            }

            return StringBuilderPool.Shared.ToStringReturn(stringBuilder);
        }

        public abstract Hero Create();

        public abstract Hero Create(Player player, SideType sideType);
    }
}

