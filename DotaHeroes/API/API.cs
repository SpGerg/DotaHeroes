using DotaHeroes.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API
{
    public class API
    {
        private static Dictionary<int, Hero> Players { get; } = new Dictionary<int, Hero>();

        private static Dictionary<string, Hero> RegisteredHeroes = new Dictionary<string, Hero>();

        /// <summary>
        /// Set or add player from id.
        /// </summary>
        public static void SetOrAddPlayer(int id, Hero hero)
        {
            Players[id] = hero;
        }

        /// <summary>
        /// Register hero.
        /// </summary>
        public static void RegisterHero(Hero hero)
        {
            RegisteredHeroes[hero.HeroName] = hero;
        }

        /// <summary>
        /// Return registered heroes (new copy)
        /// </summary>
        public static IReadOnlyDictionary<string, Hero> GetRegisteredHeroes()
        {
            return new Dictionary<string, Hero>(RegisteredHeroes);
        }

        /// <summary>
        /// Return of all players-heroes (new copy)
        /// </summary>
        public static IReadOnlyDictionary<int, Hero> GetHeroes()
        {
            return new Dictionary<int, Hero>(Players);
        }

        /// <summary>
        /// Get hero or default.
        /// </summary>
        public static Hero GetHeroOrDefault(int id)
        {
            return Players.FirstOrDefault(_player => _player.Key == id).Value;
        }
    }
}
