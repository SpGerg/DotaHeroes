using DotaHeroes.API.Enums;
using DotaHeroes.API.Events.EventArgs.Interfaces;
using DotaHeroes.API.Interfaces;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Events.EventArgs.Hero
{
    /// <summary>
    /// Contains all information before a hero attack.
    /// </summary>
    public class HeroAttackingEventArgs : IHeroEvent, IDamage, IDeniableEvent
    {
        public Features.Hero Hero { get; }

        public Features.Hero Target { get; }

        public int Damage { get; set; }

        public DamageType DamageType { get; set; }

        public bool IsAllowed { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HeroAttackingEventArgs" /> class.
        /// </summary>
        /// <param name="hero"><inheritdoc cref="Hero" /></param>
        /// <param name="target"><inheritdoc cref="Target" /></param>
        /// <param name="damage"><inheritdoc cref="Damage" /></param>
        /// <param name="damageType"><inheritdoc cref="DamageType" /></param>
        /// <param name="isAllowed"><inheritdoc cref="IsAllowed" /></param>
        public HeroAttackingEventArgs(Features.Hero hero, Features.Hero target, int damage, DamageType damageType, bool isAllowed)
        {
            Hero = hero;
            Target = target;
            Damage = damage;
            DamageType = damageType;
            IsAllowed = isAllowed;
        }
    }
}
