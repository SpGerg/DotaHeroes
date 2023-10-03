using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Statistics
{
    public class ArmorStatistics
    {
        public decimal BaseArmor { get; set; }

        public decimal ExtraArmor { get; set; }

        public ArmorStatistics() { }

        public ArmorStatistics(decimal baseArmor, decimal moreArmor)
        {
            BaseArmor = baseArmor;
            ExtraArmor = moreArmor;
        }

        public override string ToString()
        {
            if (ExtraArmor > 0)
            {
                return $"Armor: {Math.Round(BaseArmor, 1)} + <color=Green>{Math.Round(ExtraArmor, 1)}</color>";
            }

            return $"Armor: {Math.Round(BaseArmor, 1)}";
        }
    }
}
