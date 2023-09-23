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

        public float ExtraArmor { get; set; }

        public ArmorStatistics() { }

        public ArmorStatistics(float baseArmor, float moreArmor)
        {
            BaseArmor = baseArmor;
            ExtraArmor = moreArmor;
        }

        public override string ToString()
        {
            if (ExtraArmor > 0)
            {
                return $"Armor: {BaseArmor} + <color=Green>{ExtraArmor}</color>";
            }

            return $"Armor: {BaseArmor}";
        }
    }
}
