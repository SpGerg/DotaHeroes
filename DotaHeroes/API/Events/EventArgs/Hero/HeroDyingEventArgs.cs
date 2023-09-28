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
    /// Contains all information before hero dying.
    /// </summary>
    public class HeroDyingEventArgs : IHeroEvent, IDeniableEvent
    {
        public Features.Hero Hero { get; }

        public bool IsAllowed { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HeroDyingEventArgs" /> class.
        /// </summary>
        /// <param name="hero"><inheritdoc cref="Hero" /></param>
        /// <param name="isAllowed"><inheritdoc cref="IsAllowed" /></param>
        public HeroDyingEventArgs(Features.Hero hero, bool isAllowed)
        {
            Hero = hero;
            IsAllowed = isAllowed;
        }
    }
}
