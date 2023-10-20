using DotaHeroes.API.Interfaces;

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
