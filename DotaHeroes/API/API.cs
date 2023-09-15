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

        public static void AddPlayer(string userId, Hero hero)
        {
            Players.Add(userId, hero);
        }

        public static Hero GetHeroOrDefault(string userId)
        {
            return Players.FirstOrDefault(_player => _player.Key == userId).Value;
        }
    }
}
