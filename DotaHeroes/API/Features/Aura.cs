using DotaHeroes.API.Enums;
using DotaHeroes.API.Events.Handlers;
using Exiled.API.Features;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DotaHeroes.API.Features
{
    public abstract class Aura : Ability
    {
        public abstract float Radius { get; set; }

        public override AbilityType AbilityType => AbilityType.None;

        public override string Lore => string.Empty;

        public virtual Ability Ability { get; set; }

        public virtual float LingerDuration { get; }

        public virtual float UpdateTime { get; } = 3;

        public List<Hero> Heroes { get; set; }

        public bool IsActive {
            get
            {
                return isActive;
            }
            set
            {
                isActive = value;

                if (isActive && Owner != null)
                {
                    Timing.RunCoroutine(AuraCoroutine());
                }
            }
        }

        private bool isActive;

        protected Aura() : base()
        {
            Heroes = new List<Hero>();
        }

        public Aura(Hero owner) : base(owner)
        {
            Heroes = new List<Hero>();

            IsActive = true;
        }

        public virtual void Added(Hero hero) { }

        public virtual void Removed(Hero hero) { }

        public bool IsInsideAura(Hero hero)
        {
            if (Vector3.Distance(hero.Player.Position, Owner.Player.Position) < Radius)
            {
                return true;
            }

            return false;
        }

        public override void Stop()
        {
            IsActive = false;
        }

        private IEnumerator<float> AuraCoroutine()
        {
            while (IsActive)
            {
                if (Owner.IsHeroDead)
                {
                    continue;
                }

                foreach (var hero in DTAPI.GetHeroes().Values)
                {
                    if (Plugin.Instance.Config.Debug) Log.Info(hero.Player.Nickname);

                    if (TargetType.HasFlag(TargetType.ToEnemy))
                    {
                        if (hero.SideType == Owner.SideType && !TargetType.HasFlag(TargetType.ToFriend)) continue;
                    }
                    else if (TargetType.HasFlag(TargetType.ToFriend))
                    {
                        if (hero.SideType != Owner.SideType && !TargetType.HasFlag(TargetType.ToEnemy)) continue;
                    }

                    if (IsInsideAura(hero) && !Heroes.Contains(hero))
                    {
                        Added(hero);

                        Heroes.Add(hero);
                    }

                    if (!IsInsideAura(hero) && Heroes.Contains(hero))
                    {
                        if (LingerDuration != 0)
                        {
                            Timing.RunCoroutine(LingerDurationCoroutine(hero));

                            continue;
                        }
                    }
                }

                yield return Timing.WaitForSeconds(UpdateTime);
            }

            foreach (var hero in Heroes)
            {
                if (LingerDuration != 0)
                {
                    Timing.RunCoroutine(LingerDurationCoroutine(hero));

                    continue;
                }

                Removed(hero);

                Heroes.Remove(hero);
            }
        }

        private IEnumerator<float> LingerDurationCoroutine(Hero hero)
        {
            yield return Timing.WaitForSeconds(LingerDuration);

            if (Heroes.Contains(hero)) yield break;

            Removed(hero);

            Heroes.Remove(hero);
        }
    }
}
