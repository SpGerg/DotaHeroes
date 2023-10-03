using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Features.Serializables
{
    public class AbilitySerializable
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Dictionary<string, List<float>> Values { get; set; }

        public AbilitySerializable()
        { 
            Name = string.Empty;
            Description = string.Empty;
            Values = new();
        }

        public AbilitySerializable(string name, string description, Dictionary<string, List<float>> values)
        { 
            Name = name;
            Description = description;
            Values = values;
        }
    }
}
