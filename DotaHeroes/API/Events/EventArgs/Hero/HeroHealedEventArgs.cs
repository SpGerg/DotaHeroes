using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Events.EventArgs.Hero
{
    /// <summary>
    /// Contains all information after hero healing.
    /// </summary>
    public class HeroHealedEventArgs
    {
        public Features.Hero Hero { get; }

        public Features.Hero Healer { get; }

        public float Heal { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HeroHealedEventArgs" /> class.
        /// </summary>
        /// <param name="hero"><inheritdoc cref="Hero" /></param>
        /// <param name="healer"><inheritdoc cref="Healer" /></param>
        /// <param name="heal"><inheritdoc cref="Heal" /></param>
        public HeroHealedEventArgs(Features.Hero hero, Features.Hero healer, float heal)
        {
            Hero = hero;
            Healer = healer;
            Heal = heal;
        }
    }
}
