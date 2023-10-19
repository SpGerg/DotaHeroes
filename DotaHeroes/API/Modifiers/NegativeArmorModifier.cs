using DotaHeroes.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Modifiers
{
    public class NegativeArmorModifier : INegativeArmorModifier
    {
        public decimal NegativeArmor { get; set; }

        public NegativeArmorModifier(decimal negativeArmor)
        {
            NegativeArmor = negativeArmor;
        }
    }
}
