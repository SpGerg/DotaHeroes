using DotaHeroes.API.Events.EventArgs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Events.EventArgs.Hero
{
    /// <summary>
    /// Contains all information after hero dying.
    /// </summary>
    public class HeroDiedEventArgs : IHeroEvent
    {
        public Features.Hero Hero { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HeroDiedEventArgs" /> class.
        /// </summary>
        /// <param name="hero"><inheritdoc cref="Hero" /></param>
        public HeroDiedEventArgs(Features.Hero hero)
        {
            Hero = hero;
        }
    }
}
