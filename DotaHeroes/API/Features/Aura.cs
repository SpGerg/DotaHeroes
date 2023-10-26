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
    public abstract class Aura : PassiveAbility
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

                if (isActive)
                {
                    Timing.RunCoroutine(AuraCoroutine());
                }
            }
        }

        private bool isActive;

        public Aura()
        {

        }

        public Aura(Hero owner)
        {
            Heroes = new List<Hero>();
            RegisterOwner(owner);

            IsActive = true;
        }

        public virtual void Added(Hero hero) { }

        public virtual void Removed(Hero hero) { }

        public override void Register(Hero owner) { }

        public override void Unregister(Hero owner) { }

        public bool IsInsideAura(Hero hero)
        {
            if (Vector3.Distance(hero.Player.Position, Owner.Player.Position) < Radius)
            {
                return true;
            }

            return false;
        }

        public override void Stop(Hero hero) { }

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
                    if (Plugin.Instance.Config.Debug) Log.Info(hero);

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

                        Removed(hero);

                        Heroes.Remove(hero);
                    }
                }

                yield return Timing.WaitForSeconds(UpdateTime);
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
