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
    /// Contains all information before hero healing.
    /// </summary>
    public class HeroHealingEventArgs : IHeroEvent, IDeniableEvent
    {
        public Features.Hero Hero { get; }

        public Features.Hero Healer { get; }

        public float Heal { get; set; }

        public bool IsAllowed { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HeroHealingEventArgs" /> class.
        /// </summary>
        /// <param name="hero"><inheritdoc cref="Hero" /></param>
        /// <param name="healer"><inheritdoc cref="Healer" /></param>
        /// <param name="heal"><inheritdoc cref="Heal" /></param>
        /// <param name="isAllowed"><inheritdoc cref="IsAllowed" /></param>
        public HeroHealingEventArgs(Features.Hero hero, Features.Hero healer, float heal, bool isAllowed)
        {
            Hero = hero;
            Healer = healer;
            Heal = heal;
            IsAllowed = isAllowed;
        }
    }
}
