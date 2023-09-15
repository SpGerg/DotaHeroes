using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Statistics
{
    public class SpeedStatistics
    {
        public sbyte Speed { get; set; }

        public SpeedStatistics(sbyte speed)
        {
            Speed = speed;
        }

        public override string ToString()
        {
            return $"Speed: {Speed}";
        }
    }
}
