using DotaHeroes.API.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Features.Serializables
{
    public class ItemSerializable
    {
        public string Name { get; }

        public string Description { get; }

        public string Lore { get; }

        public string Ability { get; }

        public Dictionary<StatisticsType, Value> Statistics { get; }

        public ItemSerializable()
        {
            Statistics = new Dictionary<StatisticsType, Value>();
        }

        public ItemSerializable(string name, string description, string lore, string ability, Dictionary<StatisticsType, Value> statistics)
        {
            Name = name;
            Description = description;
            Lore = lore;
            Ability = ability;
            Statistics = statistics;
        }
    }
}
