using DotaHeroes.API.Interfaces;
using System;
using System.Collections.Generic;

namespace DotaHeroes.API.Statistics
{
    public class ArmorStatistics
    {
        public double BaseArmor { get; set; }

        public List<IArmorModifier> ArmorModifiers { get; set; }

        public List<INegativeArmorModifier> NegativeArmorModifiers { get; set; }

        public ArmorStatistics()
        {
            ArmorModifiers = new List<IArmorModifier>();
            NegativeArmorModifiers = new List<INegativeArmorModifier>();
        }

        public ArmorStatistics(double baseArmor)
        {
            BaseArmor = baseArmor;
            ArmorModifiers = new List<IArmorModifier>();
            NegativeArmorModifiers = new List<INegativeArmorModifier>();
        }

        public ArmorStatistics(double baseArmor, List<IArmorModifier> armorModifiers, List<INegativeArmorModifier> negativeArmorModifiers)
        {
            BaseArmor = baseArmor;
            ArmorModifiers = armorModifiers;
            NegativeArmorModifiers = negativeArmorModifiers;
        }

        public double GetPhysicalDamage(double damage, double agility)
        {
            var armor = GetBaseArmor(agility);
            double armor_percent = (0.052 * armor) / (0.9 + 0.048 * armor);
            return (damage - ((damage / 100) * armor_percent));
        }

        public double GetBaseArmor(double agility)
        {
            return (BaseArmor + (agility / 6)) * (1 - GetNegativeArmorFromModifiers() / 10);
        }

        public double GetArmorFromModifiers()
        {
            double result = 0;

            foreach (var armor in ArmorModifiers)
            {
                result += armor.Armor;
            }

            return result;
        }

        public double GetNegativeArmorFromModifiers()
        {
            double result = 0;

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

        public string ToString(double agility)
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
