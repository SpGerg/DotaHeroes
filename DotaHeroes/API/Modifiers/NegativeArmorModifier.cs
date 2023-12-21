using DotaHeroes.API.Interfaces;

namespace DotaHeroes.API.Modifiers
{
    public class NegativeArmorModifier : INegativeArmorModifier
    {
        public double NegativeArmor { get; set; }

        public NegativeArmorModifier(double negativeArmor)
        {
            NegativeArmor = negativeArmor;
        }
    }
}
