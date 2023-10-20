using DotaHeroes.API.Enums;
using DotaHeroes.API.Events.EventArgs.Interfaces;
using DotaHeroes.API.Features;
using System.Collections.Generic;

namespace DotaHeroes.API.Events.EventArgs.Hero
{
    /// <summary>
    /// Contains all information after hero dispelled effects.
    /// </summary>
    public class HeroDispelledEventArgs : IHeroEvent
    {
        public Features.Hero Hero { get; }

        public Features.Hero Dispeller { get; }

        public List<Effect> EffectsToDispel { get; set; }

        public DispelType DispelType { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HeroDispelledEventArgs" /> class.
        /// </summary>
        /// <param name="hero"><inheritdoc cref="Hero" /></param>
        /// <param name="dispeller"><inheritdoc cref="Dispeller" /></param>
        /// <param name="effectsToDispel"><inheritdoc cref="EffectsToDispel" /></param>
        /// <param name="dispelType"><inheritdoc cref="DispelType" /></param>
        public HeroDispelledEventArgs(Features.Hero hero, Features.Hero dispeller, List<Effect> effectsToDispel, DispelType dispelType)
        {
            Hero = hero;
            Dispeller = dispeller;
            EffectsToDispel = effectsToDispel;
            DispelType = dispelType;
        }
    }
}
