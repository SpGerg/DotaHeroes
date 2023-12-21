using DotaHeroes.API.Enums;
using DotaHeroes.API.Events.EventArgs.Interfaces;
using DotaHeroes.API.Interfaces;

namespace DotaHeroes.API.Events.EventArgs.Hero
{
    /// <summary>
    /// Contains all information after a hero attack.
    /// </summary>
    public class HeroAttackedEventArgs : IHeroEvent, IDamage
    {
        public Features.Hero Hero { get; }

        public Features.Hero Target { get; }

        public double Damage { get; set; }

        public DamageType DamageType { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HeroAttackingEventArgs" /> class.
        /// </summary>
        /// <param name="hero"><inheritdoc cref="Hero" /></param>
        /// <param name="target"><inheritdoc cref="Target" /></param>
        /// <param name="damage"><inheritdoc cref="Damage" /></param>
        /// <param name="damageType"><inheritdoc cref="DamageType" /></param>
        public HeroAttackedEventArgs(Features.Hero hero, Features.Hero target, double damage, DamageType damageType)
        {
            Hero = hero;
            Target = target;
            Damage = damage;
            DamageType = damageType;
        }
    }
}
