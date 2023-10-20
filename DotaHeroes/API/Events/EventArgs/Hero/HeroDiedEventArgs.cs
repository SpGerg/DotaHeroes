using DotaHeroes.API.Events.EventArgs.Interfaces;

namespace DotaHeroes.API.Events.EventArgs.Hero
{
    /// <summary>
    /// Contains all information after hero dying.
    /// </summary>
    public class HeroDiedEventArgs : IHeroEvent
    {
        public Features.Hero Hero { get; }

        public Features.Hero Killer { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HeroDiedEventArgs" /> class.
        /// </summary>
        /// <param name="hero"><inheritdoc cref="Hero" /></param>
        public HeroDiedEventArgs(Features.Hero hero, Features.Hero killer)
        {
            Hero = hero;
            Killer = killer;
        }
    }
}
