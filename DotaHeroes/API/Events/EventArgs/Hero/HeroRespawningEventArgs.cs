using DotaHeroes.API.Events.EventArgs.Interfaces;
using Exiled.Events.EventArgs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Events.EventArgs.Hero
{
    /// <summary>
    /// Contains all information before hero respawning.
    /// </summary>
    public class HeroRespawningEventArgs : IHeroEvent, IDeniableEvent
    {
        public Features.Hero Hero { get; }

        public Features.Hero Killer { get; }

        public bool IsAllowed { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HeroRespawningEventArgs" /> class.
        /// </summary>
        /// <param name="hero"><inheritdoc cref="Hero" /></param>
        /// <param name="killer"><inheritdoc cref="Killer" /></param>
        /// <param name="isAllowed"><inheritdoc cref="IsAllowed" /></param>
        public HeroRespawningEventArgs(Features.Hero hero, Features.Hero killer, bool isAllowed)
        {
            Hero = hero;
            Killer = killer;
            IsAllowed = isAllowed;
        }
    }
}
