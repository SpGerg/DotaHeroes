using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Statistics
{
    public class ResistanceStatistics
    {
        public float MagicResistance { get; set; }

        public float EffectResistance { get; set; }

        public ResistanceStatistics() { }

        public ResistanceStatistics(float magicResistance, float effectResistance)
        {
            MagicResistance = magicResistance;
            EffectResistance = effectResistance;
        }
    }
}
