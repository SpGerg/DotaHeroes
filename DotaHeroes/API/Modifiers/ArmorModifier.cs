using DotaHeroes.API.Interfaces;

namespace DotaHeroes.API.Modifiers
{
    public class ArmorModifier : IArmorModifier
    {
        public double Armor { get; set; }

        public ArmorModifier(double armor)
        {
            Armor = armor;
        }
    }
}
