using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Statistics
{
    public class ArmorStatistics
    {
        public float BaseArmor { get; set; }

        public float MoreArmor { get; set; }

        public ArmorStatistics() { }

        public ArmorStatistics(float baseArmor, float moreArmor)
        {
            BaseArmor = baseArmor;
            MoreArmor = moreArmor;
        }

        public override string ToString()
        {
            if (MoreArmor > 0)
            {
                return $"Armor: {BaseArmor} + <color=Green>{MoreArmor}</color>";
            }

            return $"Armor: {BaseArmor}";
        }
    }
}
