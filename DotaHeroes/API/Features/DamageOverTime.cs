using DotaHeroes.API.Enums;
using DotaHeroes.API.Interfaces;
using MEC;
using System.Collections.Generic;

namespace DotaHeroes.API.Features
{
    public class DamageOverTime : IDamage
    {
        public Hero Hero { get; }

        public Hero Attacker { get; }

        public double Damage { get; set; }

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
        public DamageOverTime(Hero hero, double damage, DamageType damageType, int timesDamage, float interval, Hero attacker = null)
        {
            Hero = hero;
            Damage = damage;
            DamageType = damageType;
            TimesDamage = timesDamage;
            Interval = interval;
            Attacker = attacker;

            if (TimesDamage <= -1)
            {
                TimesDamage = int.MaxValue;
            }
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
            for (int i = 0; i < TimesDamage; i++)
            {
                if (!IsEnabled || Hero.IsHeroDead) yield break;

                if (Attacker == null)
                {
                    Hero.TakeDamage(Damage, DamageType);
                }
                else
                {
                    Hero.TakeDamage(Attacker, Damage, DamageType);
                }

                yield return Timing.WaitForSeconds(Interval);
            }
        }
    }
}
