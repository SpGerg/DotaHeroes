using DotaHeroes.API.Interfaces;

namespace DotaHeroes.API.Modifiers
{
    public class ArmorModifier : IArmorModifier
    {
        public decimal Armor { get; set; }

        public ArmorModifier(decimal armor)
        {
            Armor = armor;
        }
    }
}
