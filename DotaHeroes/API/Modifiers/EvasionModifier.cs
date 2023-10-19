using DotaHeroes.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Modifiers
{
    public class EvasionModifier : IEvasionModifier
    {
        public decimal Evasion { get; set; }

        public EvasionModifier(decimal evasion)
        {
            Evasion = evasion;
        }
    }
}
