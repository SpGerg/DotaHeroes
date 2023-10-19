using DotaHeroes.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Modifiers
{
    public class ArmorModifier : IArmorModifier
    {
        public decimal Armor { get; set; }

        public ArmorModifier(decimal armor)
        {
            Armor = armor;
        }
    }
}
