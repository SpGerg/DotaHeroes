using DotaHeroes.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Modifiers
{
    public class BlindModifier : IBlindModifier
    {
        public decimal Blind { get; set; }

        public BlindModifier(decimal blind)
        {
            Blind = blind;
        }
    }
}
