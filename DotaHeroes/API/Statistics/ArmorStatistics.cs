using DotaHeroes.API.Interfaces;
using System;
using System.Collections.Generic;

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
            return (BaseArmor + (agility / 6)) * (1 - GetNegativeArmorFromModifiers() / 10);
        }

        public decimal GetArmorFromModifiers()
        {
            decimal result = 0;

            foreach (var armor in ArmorModifiers)
            {
                result += armor.Armor;
            }

            return result;
        }

        public decimal GetNegativeArmorFromModifiers()
        {
            decimal result = 0;

            foreach (var negativeArmor in NegativeArmorModifiers)
            {
                result += negativeArmor.NegativeArmor;
            }

            return result;
        }

        public override string ToString()
        {
            return $"Armor: {Math.Round(BaseArmor, 1)}";
        }

        public string ToString(decimal agility)
        {
            var armor = GetArmorFromModifiers();

            if (armor > 0)
            {
                return $"Armor: {Math.Round(GetBaseArmor(agility))} + <color=green>{armor}</color>";
            }

            return $"Armor: {Math.Round(GetBaseArmor(agility))}";
        }
    }
}
