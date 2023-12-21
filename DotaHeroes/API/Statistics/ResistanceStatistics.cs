using DotaHeroes.API.Interfaces;
using System;
using System.Collections.Generic;

namespace DotaHeroes.API.Statistics
{
    public class ResistanceStatistics
    {
        public double BaseMagicResistance { get; set; }

        public double BaseEffectResistance { get; set; }

        public List<IResistanceModifier> ResistanceModifiers { get; set; }

        public const double BaseResistance = 25;

        public ResistanceStatistics()
        {
            BaseMagicResistance = BaseResistance;
            BaseEffectResistance = BaseResistance;
            ResistanceModifiers = new List<IResistanceModifier>();
        }

        public ResistanceStatistics(double magicResistance, double baseEffectResistance)
        {
            BaseMagicResistance = magicResistance;
            BaseEffectResistance = baseEffectResistance;
            ResistanceModifiers = new List<IResistanceModifier>();
        }

        public double GetMagicalDamage(double damage, double intelligence)
        {
            double percent = (damage / 100);
            return damage - percent * GetMagicResistance(intelligence);
        }

        public double GetEffectResistance()
        {
            double baseEffectResistance = (BaseEffectResistance / 100);
            double effectResistanceFromModifiers = GetEffectResistanceFromModifiers();

            return (1 - ((1 - baseEffectResistance) * effectResistanceFromModifiers)) * 100;
        }

        public double GetEffectDuration(double originalDuration)
        {
            return originalDuration * ((100 - GetEffectResistance()) / 100);
        }

        public double GetMagicResistance(double intelligence)
        {
            double baseMagicResistance = (BaseMagicResistance / 100);
            double magicResistanceFromModifiers = GetMagicResistanceFromModifiers();

            return ((1 - ((1 - baseMagicResistance) * magicResistanceFromModifiers)) + ((intelligence / 10) / 100)) * 100;
        }

        private double GetEffectResistanceFromModifiers()
        {
            double result = 1;

            foreach (var modifier in ResistanceModifiers)
            {
                result *= 1 - (modifier.EffectResistance / 100);
            }

            return result;
        }

        private double GetMagicResistanceFromModifiers()
        {
            double result = 1;

            foreach (var modifier in ResistanceModifiers)
            {
                result *= 1 - (modifier.MagicResistance / 100);
            }

            return result;
        }

        public override string ToString()
        {
            return $"Effect resistance: {GetEffectResistance()}";
        }

        public string ToString(double intelligence)
        {
            return $"Magic resistance: {Math.Round(GetMagicResistance(intelligence), 1)} Effect resistance: {Math.Round(GetEffectResistance(), 1)}";
        }
    }
}
