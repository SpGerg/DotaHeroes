using DotaHeroes.API.Enums;
using DotaHeroes.API.Events.EventArgs.Interfaces;
using DotaHeroes.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Events.EventArgs.Hero
{
    /// <summary>
    /// Contains all information after hero taking damage.
    /// </summary>
    public class HeroTakedDamageEventArgs : IHeroEvent, IDamage
    {
        public Features.Hero Hero { get; }

        public Features.Hero Attacker { get; }

        public int Damage { get; set; }

        public DamageType DamageType { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HeroTakedDamageEventArgs" /> class.
        /// </summary>
        /// <param name="hero"><inheritdoc cref="Hero" /></param>
        /// <param name="attacker"><inheritdoc cref="Attacker" /></param>
        /// <param name="damage"><inheritdoc cref="Damage" /></param>
        /// <param name="damageType"><inheritdoc cref="DamageType" /></param>
        public HeroTakedDamageEventArgs(Features.Hero hero, Features.Hero attacker, int damage, DamageType damageType)
        {
            Hero = hero;
            Attacker = attacker;
            Damage = damage;
            DamageType = damageType;
        }
    }
}
