using DotaHeroes.API.Enums;
using DotaHeroes.API.Features.Components;
using DotaHeroes.API.Features.Objects;
using DotaHeroes.API.Interfaces;
using DotaHeroes.API.Statistics;
using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using NorthwoodLib.Pools;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotaHeroes.API.Features
{
    public abstract class Hero
    {
        public abstract string HeroName { get; }

        public abstract HeroClassType HeroClassType { get; set; }

        public virtual List<Ability> Abilities => new List<Ability>();

        public virtual HeroStatistics HeroStatistics => new HeroStatistics(AttributeType.None);

        public virtual RoleTypeId Model => RoleTypeId.Tutorial;

        public Player Player { get; }

        public SideType SideType { get; set; } = SideType.None;

        public float ProjectileSpeed { get; set; } = 0.6f;

        public List<ProjectileObject> ProjectilesFollow { get; }

        public HeroController HeroController { get; }

        public int Level { get; set; }

        private List<Effect> Effects { get; }

        private bool isDead;

        public bool IsDead
        {
            get
            {
                return isDead;
            }
            set
            {
                isDead = value;

                if (isDead)
                {
                    ApplyDispel(DispelType.Dead);

                    Player.Role.Set(RoleTypeId.Spectator);
                }
                else
                {
                    Player.Role.Set(Model);
                }
            }
        }

        public Hero(Player player)
        {
            Player = player;

            Effects = new List<Effect>();
            API.SetOrAddPlayer(player.UserId, this);

            HeroController = Player.GameObject.GetComponent<HeroController>();
        }

        public Hero(Player player, SideType sideType)
        {
            Player = player;
            SideType = sideType;

            Effects = new List<Effect>();

            API.SetOrAddPlayer(player.UserId, this);

            HeroController = Player.GameObject.GetComponent<HeroController>();
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

        public void EnableEffect<T>() where T : Effect, new()
        {
            if (TryGetEffect(out T result))
            {
                return;
            }

            var effect = new T();
            effect.Enable();
            Effects.Add(effect);
        }

        public void EnableEffect(Effect _effect)
        {
            if (TryGetEffect(_effect, out Effect result))
            {
                return;
            }

            _effect.Enable();
            Effects.Add(_effect);
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

        public void ExecuteEffect(Effect effect)
        {
            if (!TryGetEffect(effect, out Effect result))
            {
                return;
            }

            result.Execute();
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

        public Effect GetEffectOrDefault<T>() where T : Effect, new()
        {
            return Effects.FirstOrDefault(_effect => _effect is T);
        }

        public Effect GetEffectOrDefault(Effect effect)
        {
            return Effects.FirstOrDefault(_effect => _effect.GetType() == effect.GetType());
        }

        public List<Effect> GetEffects()
        {
            return new List<Effect>(Effects);
        }

        public bool TryGetEffect<T>(out T result) where T : Effect, new()
        {
            var effect = GetEffectOrDefault<T>();

            if (effect == default)
            {
                result = null;

                return false;
            }

            result = (T)effect;

            return true;
        }

        public bool TryGetEffect(Effect _effect, out Effect result)
        {
            var effect = GetEffectOrDefault(_effect);

            if (effect == default)
            {
                result = null;

                return false;
            }

            result = effect;

            return true;
        }
    }
}

