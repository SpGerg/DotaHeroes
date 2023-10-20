using DotaHeroes.API.Events.EventArgs.Interfaces;

namespace DotaHeroes.API.Events.EventArgs.Hero
{
    /// <summary>
    /// Contains all information after hero respawning.
    /// </summary>
    public class HeroRespawnedEventArgs : IHeroEvent
    {
        public Features.Hero Hero { get; }

        public Features.Hero Killer { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HeroRespawnedEventArgs" /> class.
        /// </summary>
        /// <param name="hero"><inheritdoc cref="Hero" /></param>
        /// <param name="killer"><inheritdoc cref="Killer" /></param>
        public HeroRespawnedEventArgs(Features.Hero hero, Features.Hero killer)
        {
            Hero = hero;
            Killer = killer;
        }
    }
}
