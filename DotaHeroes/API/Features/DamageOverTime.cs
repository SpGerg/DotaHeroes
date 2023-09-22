using DotaHeroes.API.Enums;
using DotaHeroes.API.Interfaces;
using Exiled.API.Features;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Features
{
    public class DamageOverTime : IDamage
    {
        public int Damage { get; set; }

        public DamageType DamageType { get; set; }

        public int TimesDamage { get; set; }

        public float Interval { get; set; }

        public bool IsEnabled { get; set; }

        public DamageOverTime(int damage, DamageType damageType, int timesDamage, float interval)
        {
            Damage = damage;
            DamageType = damageType;
            TimesDamage = timesDamage;
            Interval = interval;
        }

        public void Run(Hero hero)
        {
            IsEnabled = false;
            IsEnabled = true;

            Timing.RunCoroutine(DamageCoroutine(hero));
        }
        
        private IEnumerator<float> DamageCoroutine(Hero hero)
        {
            if (TimesDamage <= -1)
            {
                while (true)
                {
                    if (!IsEnabled || hero.IsHeroDead) yield break;

                    hero.TakeDamage(hero, Damage, DamageType);

                    yield return Timing.WaitForSeconds(Interval);
                }
            }

            for (int i = 0;i < TimesDamage;i++)
            {
                if (!IsEnabled || hero.IsHeroDead) yield break;

                hero.TakeDamage(hero, Damage, DamageType);

                yield return Timing.WaitForSeconds(Interval);
            }
        }
    }
}
