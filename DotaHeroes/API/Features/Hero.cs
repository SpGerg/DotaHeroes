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

        public HeroStatistics HeroStatistics { get; protected set; }

        public virtual List<Ability> Abilities => new List<Ability>();

        public virtual RoleTypeId Model => RoleTypeId.Tutorial;

        public Player Player { get; private set; }

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
            set
            {
                isHeroDead = value;

                if (isHeroDead)
                {
                    ApplyDispel(DispelType.Dead);

                    Player.Role.Set(RoleTypeId.Spectator);

                    Dead();
                }
                else
                {
                    Player.Role.Set(Model);

                    Respawn();
                }
            }
        }

        private List<Effect> Effects { get; }

        private bool isHeroDead;

        public Dictionary<string, object> Values { get; }

        public Hero()
        {
            Player = null;
            Effects = new List<Effect>();
            Values = new Dictionary<string, object>();
        }

        public Hero(Player player, SideType sideType)
        {
            Player = player;
            SideType = sideType;

            Effects = new List<Effect>();
            Values = new Dictionary<string, object>();

            API.SetOrAddPlayer(player.UserId, this);

            HeroController = Player.GameObject.GetComponent<HeroController>();

            foreach (var ability in Abilities)
            {
                if (ability is IValues)
                {
                    var values = (ability as IValues).Values;
                    if (values.ContainsKey("cooldown"))
                    {
                        Cooldowns.AddCooldown(player.UserId, new CooldownInfo(ability.Name, (int)values["cooldown"][ability.Level]));
                    }
                }
            }
        }

        public void ApplyDispel(DispelType dispelType)
        {
            if (dispelType == DispelType.NotDispelling)
            {
                return;
            }

            if (dispelType == DispelType.Dead)
            {
                foreach (var effect in Effects)
                {
                    if (effect.DispelType != DispelType.None)
                    {
                        effect.Dispel();
                        DisableEffect(effect);
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
                        effect.Dispel();
                        DisableEffect(effect);
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
                        effect.Dispel();
                        DisableEffect(effect);
                    }
                }

                return;
            }
        }

        public bool ExecuteAbility(string name)
        {
            var ability = Abilities.First(ability => ability.Name == name);

            if (ability == default)
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

        public bool ExecuteAbility<T>() where T : Ability, new()
        {
            var ability = new T();

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

        public T EnableEffect<T>() where T : Effect, new()
        {
            if (TryGetEffect(out T result))
            {
                return result;
            }

            var effect = new T();
            effect.Enable();
            Effects.Add(effect);

            return effect;
        }

        public Effect EnableEffect(Effect _effect)
        {
            if (TryGetEffect(_effect, out Effect result))
            {
                return result;
            }

            _effect.Enable();
            Effects.Add(_effect);

            return _effect;
        }

        public void ExecuteEffect<T>() where T : Effect, new()
        {
            if (!TryGetEffect(out T result))
            {
                return;
            }

            result.Execute();
        }

        public void ExecuteEffect(Effect effect)
        {
            if (!TryGetEffect(effect, out Effect result))
            {
                return;
            }

            result.Execute();
        }


        public void DisableEffect<T>() where T : Effect, new()
        {
            if (!TryGetEffect(out T result))
            {
                return;
            }

            result.Disable();
            Effects.Remove(result);
        }
        public void DisableEffect(Effect effect)
        {
            if (!TryGetEffect(effect, out Effect result))
            {
                return;
            }

            result.Disable();
            Effects.Remove(result);
        }

        public T GetEffectOrDefault<T>() where T : Effect, new()
        {
            return (T)Effects.FirstOrDefault(_effect => _effect is T);
        }

        public Effect GetEffectOrDefault(Effect effect)
        {
            return Effects.FirstOrDefault(_effect => _effect.Name == effect.Name);
        }

        public List<Effect> GetEffects()
        {
            return new List<Effect>(Effects);
        }

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

        public virtual decimal TakeDamage(int damage, DamageType damageType)
        {
            var takingDamage = new HeroTakingDamageEventArgs(this, null, damage, damageType, true);
            Events.Handlers.Hero.TakingDamage.InvokeSafely(takingDamage);

            if (!takingDamage.IsAllowed)
            {
                return -1;
            }

            decimal total_damage = 0;

            switch (damageType)
            {
                case DamageType.None: break;
                case DamageType.Physical:
                    decimal armor = (decimal)(HeroStatistics.Armor.BaseArmor + HeroStatistics.Armor.ExtraArmor);
                    decimal armor_percent = (0.052m * armor) / (0.9m + 0.048m * armor);
                    total_damage = (int)(damage - ((damage / 100) * armor_percent));

                    break;
                case DamageType.Magical:
                    decimal percent = (damage / 100m);
                    total_damage = damage - percent * (decimal)HeroStatistics.Resistance.GetMagicResistance(HeroStatistics.Intelligence);

                    break;
                case DamageType.Pure:
                    total_damage = damage;
                    break;
                default:
                    total_damage = damage;
                    break;
            }

            ReduceHealthAndCheckForDead((float)total_damage);

            var takedDamage = new HeroTakedDamageEventArgs(this, null, damage, damageType);
            Events.Handlers.Hero.TakedDamage.InvokeSafely(takedDamage);

            return total_damage;
        }

        public virtual decimal TakeDamage(Hero attacker, int damage, DamageType damageType)
        {
            var takingDamage = new HeroTakingDamageEventArgs(this, attacker, damage, damageType, true);
            Events.Handlers.Hero.TakingDamage.InvokeSafely(takingDamage);

            var result = TakeDamage(damage, damageType);

            var takedDamage = new HeroTakedDamageEventArgs(this, attacker, damage, damageType);
            Events.Handlers.Hero.TakedDamage.InvokeSafely(takedDamage);

            return result;
        }


        private void ReduceHealthAndCheckForDead(float damage)
        {
            HeroStatistics.HealthAndMana.Health -= damage;

            if (HeroStatistics.HealthAndMana.Health < 0)
            {
                IsHeroDead = true;
            }
        }

        public virtual void Heal(int heal)
        {
            HeroStatistics.HealthAndMana.Health += heal;
        }

        public virtual void Attack(Hero target)
        {
            target.TakeDamage(HeroStatistics.Attack.FullDamage, DamageType.Physical);
        }

        public virtual void Dead()
        { 
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
        }

        public virtual void Respawn() { }

        public override string ToString()
        {
            var stringBuilder = StringBuilderPool.Shared.Rent();

            stringBuilder.Append("<voffset=2em>");
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

            stringBuilder.Append("</voffset>");

            return StringBuilderPool.Shared.ToStringReturn(stringBuilder);
        }

        public abstract Hero Create();

        public abstract Hero Create(Player player, SideType sideType);
    }
}

