using DotaHeroes.API.Features;
using DotaHeroes.API.Interfaces;
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

        public List<IArmorModifier> ArmorModifiers { get; set; }

        public List<INegativeArmorModifier> NegativeArmorModifiers { get; set; }

        public ArmorStatistics()
        {
            ArmorModifiers = new List<IArmorModifier>();
            NegativeArmorModifiers = new List<INegativeArmorModifier>();
        }

        public ArmorStatistics(decimal baseArmor)
        {
            BaseArmor = baseArmor;
            ArmorModifiers = new List<IArmorModifier>();
            NegativeArmorModifiers = new List<INegativeArmorModifier>();
        }

        public ArmorStatistics(decimal baseArmor, List<IArmorModifier> armorModifiers, List<INegativeArmorModifier> negativeArmorModifiers)
        {
            BaseArmor = baseArmor;
            ArmorModifiers = armorModifiers;
            NegativeArmorModifiers = negativeArmorModifiers;
        }

        public decimal GetPhysicalDamage(decimal damage, decimal agility)
        {
            var armor = GetBaseArmor(agility);
            decimal armor_percent = (0.052m * armor) / (0.9m + 0.048m * armor);
            return (damage - ((damage / 100) * armor_percent));
        }

        public decimal GetBaseArmor(decimal agility)
        {
            return (BaseArmor + (agility / 6)) * (1 - GetNegativeArmorFromModifiers() / 10) + GetArmorFromModifiers();
        }

        public decimal GetArmorFromModifiers()
        {
            float result = 0;

            foreach (var armor in ArmorModifiers)
            {
                result += armor.Armor;
            }

            return (decimal)result;
        }

        public decimal GetNegativeArmorFromModifiers()
        {
            float result = 0;

            foreach (var negativeArmor in NegativeArmorModifiers)
            {
                result += negativeArmor.NegativeArmor;
            }

            return (decimal)result;
        }

        public override string ToString()
        {
            return $"Armor: {Math.Round(BaseArmor, 1)}";
        }

        public string ToString(decimal agility)
        {
            return $"Armor: {Math.Round(GetBaseArmor(agility))}";
        }
    }
}
