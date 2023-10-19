using DotaHeroes.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Modifiers
{
    public class SpeedModifier : ISpeedModifier
    {
        public sbyte Speed { get; set; }

        public SpeedModifier(sbyte speed)
        {
            Speed = speed;
        }
    }
}
