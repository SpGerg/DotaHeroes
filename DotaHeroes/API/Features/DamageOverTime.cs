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
        public Hero Hero { get; set; }

        public decimal Damage { get; set; }

        public DamageType DamageType { get; set; }

        public int TimesDamage { get; set; }

        public float Interval { get; set; }

        public bool IsEnabled { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DamageOverTime" /> class.
        /// </summary>
        /// <param name="hero"><inheritdoc cref="Hero" /></param>
        /// <param name="damage"><inheritdoc cref="Damage" /></param>
        /// <param name="damageType"><inheritdoc cref="DamageType" /></param>
        /// <param name="timesDamage"><inheritdoc cref="TimesDamage" /></param>
        /// <param name="interval"><inheritdoc cref="Interval" /></param>
        public DamageOverTime(Hero hero, decimal damage, DamageType damageType, int timesDamage, float interval)
        {
            Hero = hero;
            Damage = damage;
            DamageType = damageType;
            TimesDamage = timesDamage;
            Interval = interval;
        }

        /// <summary>
        ///     Run a damage coroutine, and stop other damage coroutine.
        /// </summary>
        /// <param name="hero">Hero</param>
        public void Run()
        {
            IsEnabled = true;

            Timing.RunCoroutine(DamageCoroutine());
        }

        private IEnumerator<float> DamageCoroutine()
        {
            if (TimesDamage <= -1)
            {
                while (true)
                {
                    if (!IsEnabled || Hero.IsHeroDead) yield break;

                    Hero.TakeDamage(Hero, Damage, DamageType);

                    yield return Timing.WaitForSeconds(Interval);
                }
            }

            for (int i = 0;i < TimesDamage;i++)
            {
                if (!IsEnabled || Hero.IsHeroDead) yield break;

                Hero.TakeDamage(Hero, Damage, DamageType);

                yield return Timing.WaitForSeconds(Interval);
            }
        }
    }
}
