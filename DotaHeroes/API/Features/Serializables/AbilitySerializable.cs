using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Features.Serializables
{
    public class AbilitySerializable
    {
        public Dictionary<string, List<decimal>> Values { get; set; }

        public AbilitySerializable()
        { 
            Values = new();
        }

        public AbilitySerializable(Dictionary<string, List<decimal>> values)
        {
            Values = values;
        }
    }
}
