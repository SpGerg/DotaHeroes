using System.Collections.Generic;

namespace DotaHeroes.API.Features.Serializables
{
    public class AbilitySerializable
    {
        public Dictionary<string, List<double>> Values { get; set; }

        public AbilitySerializable()
        { 
            Values = new();
        }

        public AbilitySerializable(Dictionary<string, List<double>> values)
        {
            Values = values;
        }
    }
}
