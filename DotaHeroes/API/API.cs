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

        private static Dictionary<string, Ability> RegisteredAbilties = new Dictionary<string, Ability>();

        /// <summary>
        /// Set or add player from id.
        /// </summary>
        public static void SetOrAddPlayer(int id, Hero hero)
        {
            Players[id] = hero;
        }

        /// <summary>
        /// Register hero
        /// </summary>
        public static void RegisterHero(Hero hero)
        {
            RegisteredHeroes[hero.HeroName] = hero;
        }

        /// <summary>
        /// Register ability
        /// </summary>
        public static void RegisterAbility(Ability ability)
        {
            RegisteredAbilties[ability.Name] = ability;
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
        /// Return registered abilties (new copy)
        /// </summary>
        public static IReadOnlyDictionary<string, Ability> GetRegisteredAbilties()
        {
            return new Dictionary<string, Ability>(RegisteredAbilties);
        }

        /// <summary>
        /// Get hero or default.
        /// </summary>
        public static Hero GetHeroOrDefault(int id)
        {
            return Players.FirstOrDefault(_player => _player.Key == id).Value;
        }

        /// <summary>
        /// Get registered hero or default.
        /// </summary>
        public static Hero GetRegisteredHeroOrDefault(string name)
        {
            return RegisteredHeroes.FirstOrDefault(_player => _player.Key == name).Value;
        }

        /// <summary>
        /// Get ability or default.
        /// </summary>
        public static Ability GetAbilityOrDefault(string name)
        {
            return RegisteredAbilties.FirstOrDefault(ability => ability.Key == name).Value;
        }

        /// <summary>
        /// Get ability or default.
        /// </summary>
        public static Ability GetAbilityOrDefaultWithIgnoreCaseAndSpaces(string name)
        {
            return RegisteredAbilties.FirstOrDefault(ability => string.Equals(ability.Key.Replace(" ", ""), name.Replace(" ", ""), StringComparison.OrdinalIgnoreCase)).Value;
        }
    }
}
