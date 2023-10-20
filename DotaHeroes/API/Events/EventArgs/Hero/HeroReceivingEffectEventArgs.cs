using DotaHeroes.API.Events.EventArgs.Interfaces;
using DotaHeroes.API.Features;
using Exiled.Events.EventArgs.Interfaces;

namespace DotaHeroes.API.Events.EventArgs.Hero
{
    /// <summary>
    /// Contains all information before hero receiving effect.
    /// </summary>
    public class HeroReceivingEffectEventArgs : IHeroEvent, IDeniableEvent
    {
        public Features.Hero Hero { get; }

        public Effect Effect { get; }

        public bool IsAllowed { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HeroReceivingEffectEventArgs" /> class.
        /// </summary>
        /// <param name="hero"><inheritdoc cref="Hero" /></param>
        /// <param name="effect"><inheritdoc cref="Effect" /></param>
        /// <param name="isAllowed"><inheritdoc cref="IsAllowed" /></param>
        public HeroReceivingEffectEventArgs(Features.Hero hero, Effect effect, bool isAllowed)
        {
            Hero = hero;
            Effect = effect;
            IsAllowed = isAllowed;
        }
    }
}
