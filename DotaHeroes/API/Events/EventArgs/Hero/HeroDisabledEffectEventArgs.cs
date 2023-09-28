using DotaHeroes.API.Events.EventArgs.Interfaces;
using DotaHeroes.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Events.EventArgs.Hero
{
    /// <summary>
    /// Contains all information after hero disabled effect.
    /// </summary>
    public class HeroDisabledEffectEventArgs : IHeroEvent
    {
        public Features.Hero Hero { get; }

        public Effect Effect { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HeroDisabledEffectEventArgs" /> class.
        /// </summary>
        /// <param name="hero"><inheritdoc cref="Hero" /></param>
        /// <param name="effect"><inheritdoc cref="Effect" /></param>
        public HeroDisabledEffectEventArgs(Features.Hero hero, Effect effect)
        {
            Hero = hero;
            Effect = effect;
        }
    }
}
