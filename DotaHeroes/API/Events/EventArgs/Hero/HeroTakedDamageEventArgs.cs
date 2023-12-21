using DotaHeroes.API.Enums;
using DotaHeroes.API.Events.EventArgs.Interfaces;
using DotaHeroes.API.Interfaces;

namespace DotaHeroes.API.Events.EventArgs.Hero
{
    /// <summary>
    /// Contains all information after hero taking damage.
    /// </summary>
    public class HeroTakedDamageEventArgs : IHeroEvent, IDamage
    {
        public Features.Hero Hero { get; }

        public Features.Hero Attacker { get; }

        public double Damage { get; set; }

        public DamageType DamageType { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HeroTakedDamageEventArgs" /> class.
        /// </summary>
        /// <param name="hero"><inheritdoc cref="Hero" /></param>
        /// <param name="attacker"><inheritdoc cref="Attacker" /></param>
        /// <param name="damage"><inheritdoc cref="Damage" /></param>
        /// <param name="damageType"><inheritdoc cref="DamageType" /></param>
        public HeroTakedDamageEventArgs(Features.Hero hero, Features.Hero attacker, double damage, DamageType damageType)
        {
            Hero = hero;
            Attacker = attacker;
            Damage = damage;
            DamageType = damageType;
        }
    }
}
