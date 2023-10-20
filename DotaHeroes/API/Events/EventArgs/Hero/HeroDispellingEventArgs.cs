using DotaHeroes.API.Enums;
using DotaHeroes.API.Events.EventArgs.Interfaces;
using DotaHeroes.API.Features;
using Exiled.Events.EventArgs.Interfaces;
using System.Collections.Generic;

namespace DotaHeroes.API.Events.EventArgs.Hero
{
    /// <summary>
    /// Contains all information before hero dispelled effects.
    /// </summary>
    public class HeroDispellingEventArgs : IHeroEvent, IDeniableEvent
    {
        public Features.Hero Hero { get; }

        public Features.Hero Dispeller { get; }

        public List<Effect> EffectsToDispel { get; set; }

        public DispelType DispelType { get; set; }

        public bool IsAllowed { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HeroDispellingEventArgs" /> class.
        /// </summary>
        /// <param name="hero"><inheritdoc cref="Hero" /></param>
        /// <param name="dispeller"><inheritdoc cref="Dispeller" /></param>
        /// <param name="effectsToDispel"><inheritdoc cref="EffectsToDispel" /></param>
        /// <param name="dispelType"><inheritdoc cref="DispelType" /></param>
        /// <param name="isAllowed"><inheritdoc cref="IsAllowed" /></param>
        public HeroDispellingEventArgs(Features.Hero hero, Features.Hero dispeller, List<Effect> effectsToDispel, DispelType dispelType, bool isAllowed)
        {
            Hero = hero;
            Dispeller = dispeller;
            EffectsToDispel = effectsToDispel;
            DispelType = dispelType;
            IsAllowed = isAllowed;
        }
    }
}
