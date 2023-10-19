using DotaHeroes.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Modifiers
{
    public class ResistanceModifier : IResistanceModifier
    {
        public decimal MagicResistance { get; set; }

        public decimal EffectResistance { get; set; }

        public ResistanceModifier(decimal magicResistance, decimal effectResistance)
        {
            MagicResistance = magicResistance;
            EffectResistance = effectResistance;
        }
    }
}
