using DotaHeroes.API.Enums;
using DotaHeroes.API.Events.EventArgs.Interfaces;
using DotaHeroes.API.Interfaces;
using Exiled.Events.EventArgs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Events.EventArgs.Hero
{
    /// <summary>
    /// Contains all information before hero taking damage.
    /// </summary>
    public class HeroTakingDamageEventArgs : IHeroEvent, IDamage, IDeniableEvent
    {
        public Features.Hero Hero { get; }

        public Features.Hero Attacker { get; }

        public int Damage { get; set; }

        public DamageType DamageType { get; set; }

        public bool IsAllowed { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HeroTakingDamageEventArgs" /> class.
        /// </summary>
        /// <param name="hero"><inheritdoc cref="Hero" /></param>
        /// <param name="attacker"><inheritdoc cref="Attacker" /></param>
        /// <param name="damage"><inheritdoc cref="Damage" /></param>
        /// <param name="damageType"><inheritdoc cref="DamageType" /></param>
        /// <param name="isAllowed"><inheritdoc cref="IsAllowed" /></param>
        public HeroTakingDamageEventArgs(Features.Hero hero, Features.Hero attacker, int damage, DamageType damageType, bool isAllowed)
        {
            Hero = hero;
            Attacker = attacker;
            Damage = damage;
            DamageType = damageType;
            IsAllowed = isAllowed;
        }
    }
}
