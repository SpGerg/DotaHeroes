using DotaHeroes.API.Events.EventArgs.Interfaces;
using DotaHeroes.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Events.EventArgs.Hero
{
    public class HeroExecutedAbilityEventArgs : IHeroEvent
    {
        public Features.Hero Hero { get; }

        public Ability Ability { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HeroExecutedAbilityEventArgs" /> class.
        /// </summary>
        /// <param name="hero"><inheritdoc cref="Hero" /></param>
        /// <param name="ability"><inheritdoc cref="Ability" /></param>
        public HeroExecutedAbilityEventArgs(Features.Hero hero, Ability ability)
        {
            Hero = hero;
            Ability = ability;
        }
    }
}
