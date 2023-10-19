using DotaHeroes.API.Events.EventArgs.Interfaces;
using DotaHeroes.API.Features;
using Exiled.Events.EventArgs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Events.EventArgs.Hero
{
    public class HeroExecutingAbilityEventArgs : IHeroEvent, IDeniableEvent
    {
        public Features.Hero Hero { get; }

        public Ability Ability { get; set; }

        public bool IsAllowed { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HeroExecutingAbilityEventArgs" /> class.
        /// </summary>
        /// <param name="hero"><inheritdoc cref="Hero" /></param>
        /// <param name="ability"><inheritdoc cref="Ability" /></param>
        public HeroExecutingAbilityEventArgs(Features.Hero hero, Ability ability, bool isAllowed)
        {
            Hero = hero;
            Ability = ability;
            IsAllowed = isAllowed;
        }
    }
}
