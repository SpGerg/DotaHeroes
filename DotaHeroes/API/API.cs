using DotaHeroes.API.Features;
using Exiled.API.Features;
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

        private static Dictionary<string, Item> RegisteredItems = new Dictionary<string, Item>();

        /// <summary>
        /// Set or add player from id.
        /// </summary>
        public static void SetOrAddPlayer(int id, Hero hero)
        {
            if (hero == default && Players.ContainsKey(id))
            {
                Players.Remove(id);
                return;
            }

            Players[id] = hero;
        }

        /// <summary>
        /// Register hero
        /// </summary>
        public static void RegisterHero(Hero hero)
        {
            RegisteredHeroes[hero.HeroName] = hero;

            Log.Info($"Hero with name {hero.HeroName} has been registered.");
        }

        /// <summary>
        /// Register ability
        /// </summary>
        public static void RegisterAbility(Ability ability)
        {
            RegisteredAbilties[ability.Name] = ability;

            Log.Info($"Ability with name {ability.Name} has been registered.");
        }

        /// <summary>
        /// Register item
        /// </summary>
        public static void RegisterItem(Item item)
        {
            RegisteredItems[item.Name] = item;

            Log.Info($"Item with name {item.Name} has been registered.");
        }

        /// <summary>
        /// Return registered heroes
        /// </summary>
        public static IReadOnlyDictionary<string, Hero> GetRegisteredHeroes()
        {
            return RegisteredHeroes;
        }

        /// <summary>
        /// Return of all players-heroes
        /// </summary>
        public static IReadOnlyDictionary<int, Hero> GetHeroes()
        {
            return Players;
        }

        /// <summary>
        /// Return registered abilties
        /// </summary>
        public static IReadOnlyDictionary<string, Ability> GetRegisteredAbilties()
        {
            return RegisteredAbilties;
        }

        /// <summary>
        /// Return registered items
        /// </summary>
        public static IReadOnlyDictionary<string, Item> GetRegisteredItems()
        {
            return RegisteredItems;
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
        /// Get item or default.
        /// </summary>
        public static Item GetItemOrDefault(string name)
        {
            return RegisteredItems.FirstOrDefault(item => item.Key == name).Value;
        }

        /// <summary>
        /// Get registered hero or default by slug.
        /// </summary>
        public static Hero GetRegisteredHeroOrDefaultBySlug(string slug)
        {
            return RegisteredHeroes.FirstOrDefault(player => player.Value.Slug == slug).Value;
        }

        /// <summary>
        /// Get ability or default.
        /// </summary>
        public static Ability GetAbilityOrDefaultBySlug(string slug)
        {
            return RegisteredAbilties.FirstOrDefault(ability => ability.Value.Slug == slug).Value;
        }

        /// <summary>
        /// Get item or default by slug.
        /// </summary>
        public static Item GetItemOrDefaultBySlug(string slug)
        {
            return RegisteredItems.FirstOrDefault(item => item.Value.Slug == slug).Value;
        }
    }
}
