using DotaHeroes.API.Enums;
using System.Collections.Generic;

namespace DotaHeroes.API.Features.Serializables
{
    public class ItemSerializable
    {
        public string Ability { get; set; }

        public List<string> Passives { get; set; }

        public int Cost { get; set; }

        public int SellCost { get; set; }

        public List<string> Ingredients { get; set; }

        public List<string> ItemsFromThisItems { get; set; }

        public Dictionary<StatisticsType, Value> Statistics { get; set; }

        public ItemSerializable()
        {
            Passives = new List<string>();
            Ingredients = new List<string>();
            Statistics = new Dictionary<StatisticsType, Value>();
            ItemsFromThisItems = new List<string>();
        }

        public ItemSerializable(int cost, int sellCost, string ability, List<string> passives, List<string> ingredients, List<string> itemsFromThisItems, Dictionary<StatisticsType, Value> statistics)
        {
            Ability = ability;
            Passives = passives;
            Cost = cost;
            SellCost = sellCost;
            Ingredients = ingredients;
            ItemsFromThisItems = itemsFromThisItems;
            Statistics = statistics;
        }
    }
}
