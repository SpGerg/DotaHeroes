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
        private static Dictionary<string, Hero> Players { get; } = new Dictionary<string, Hero>();

        private static Dictionary<string, Hero> RegisteredHeroes = new Dictionary<string, Hero>();
 
        public static void SetOrAddPlayer(string userId, Hero hero)
        {
            Players[userId] = hero;
        }

        public static void SetOrAddHero(Hero hero)
        {
            RegisteredHeroes[hero.HeroName] = hero;
        }

        public static IReadOnlyDictionary<string, Hero> GetHeroes()
        {
            return new Dictionary<string, Hero>(RegisteredHeroes);
        }

        public static Hero GetHeroOrDefault(string userId)
        {
            return Players.FirstOrDefault(_player => _player.Key == userId).Value;
        }
    }
}
