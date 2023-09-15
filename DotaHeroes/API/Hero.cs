using DotaHeroes.API.Enums;
using DotaHeroes.API.Interfaces;
using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using NorthwoodLib.Pools;
using PlayerRoles;
using System.Collections.Generic;
using System.Linq;

namespace DotaHeroes.API
{
    public abstract class Hero
    {
        public abstract string HeroName { get; }

        public virtual IReadOnlyList<Ability> Abilities => new List<Ability>();

        public virtual HeroStatistics HeroStatistics => new HeroStatistics(AttributeType.None);

        public virtual RoleTypeId Model => RoleTypeId.Tutorial;

        public Player Player { get; }

        public SideType SideType { get; set; } = SideType.None;

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
            API.AddPlayer(player.UserId, this);
        }

        public Hero(Player player, SideType sideType)
        {
            Player = player;
            SideType = sideType;

            Effects = new List<Effect>();

            API.AddPlayer(player.UserId, this);
        }

        public virtual void EnableEffect<T>() where T : Effect, new()
        {
            if (TryGetEffect(out T result))
            {
                return;
            }

            var effect = new T();
            effect.Enable();
            Effects.Add(effect);
        }

        public virtual void EnableEffect(Effect _effect)
        {
            if (TryGetEffect(_effect ,out Effect result))
            {
                return;
            }

            _effect.Enable();
            Effects.Add(_effect);
        }

        public virtual void DisableEffect<T>() where T : Effect, new()
        {
            if (!TryGetEffect(out T result))
            {
                return;
            }

            result.Disable();
            Effects.Remove(result);
        }

        public virtual void ExecuteEffect(Effect effect)
        {
            if (!TryGetEffect(effect, out Effect result))
            {
                return;
            }

            result.Execute();
        }

        public virtual void DisableEffect(Effect effect)
        {
            if (!TryGetEffect(effect, out Effect result))
            {
                return;
            }

            result.Disable();
            Effects.Remove(result);
        }

        public virtual Effect GetEffectOrDefault<T>() where T : Effect, new()
        {
            return Effects.FirstOrDefault(_effect => _effect is T);
        }

        public virtual Effect GetEffectOrDefault(Effect effect)
        {
            return Effects.FirstOrDefault(_effect => _effect.GetType() == effect.GetType());
        }

        public virtual List<Effect> GetEffects()
        {
            return new List<Effect>(Effects);
        }

        public virtual bool TryGetEffect<T>(out T result) where T : Effect, new()
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

        public virtual bool TryGetEffect(Effect _effect, out Effect result)
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

