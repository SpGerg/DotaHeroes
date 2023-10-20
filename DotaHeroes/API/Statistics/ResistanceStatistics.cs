using DotaHeroes.API.Interfaces;
using System;
using System.Collections.Generic;

namespace DotaHeroes.API.Statistics
{
    public class ResistanceStatistics
    {
        public decimal BaseMagicResistance { get; set; }

        public decimal BaseEffectResistance { get; set; }

        public List<IResistanceModifier> ResistanceModifiers { get; set; }

        public const decimal BaseResistance = 25;

        public ResistanceStatistics()
        {
            BaseMagicResistance = BaseResistance;
            BaseEffectResistance = BaseResistance;
            ResistanceModifiers = new List<IResistanceModifier>();
        }

        public ResistanceStatistics(decimal magicResistance, decimal baseEffectResistance)
        {
            BaseMagicResistance = magicResistance;
            BaseEffectResistance = baseEffectResistance;
            ResistanceModifiers = new List<IResistanceModifier>();
        }

        public decimal GetMagicalDamage(decimal damage, decimal intelligence)
        {
            decimal percent = (damage / 100m);
            return damage - percent * GetMagicResistance(intelligence);
        }

        public decimal GetEffectResistance()
        {
            decimal baseEffectResistance = (BaseEffectResistance / 100);
            decimal effectResistanceFromModifiers = GetEffectResistanceFromModifiers();

            return (1 - ((1 - baseEffectResistance) * effectResistanceFromModifiers)) * 100;
        }

        public decimal GetEffectDuration(decimal originalDuration)
        {
            return originalDuration * ((100 - GetEffectResistance()) / 100);
        }

        public decimal GetMagicResistance(decimal intelligence)
        {
            decimal baseMagicResistance = (BaseMagicResistance / 100);
            decimal magicResistanceFromModifiers = GetMagicResistanceFromModifiers();

            return ((1 - ((1 - baseMagicResistance) * magicResistanceFromModifiers)) + ((intelligence / 10) / 100)) * 100;
        }

        private decimal GetEffectResistanceFromModifiers()
        {
            decimal result = 1;

            foreach (var modifier in ResistanceModifiers)
            {
                result *= 1 - (modifier.EffectResistance / 100);
            }

            return result;
        }

        private decimal GetMagicResistanceFromModifiers()
        {
            decimal result = 1;

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

        public string ToString(decimal intelligence)
        {
            return $"Magic resistance: {Math.Round(GetMagicResistance(intelligence), 1)} Effect resistance: {Math.Round(GetEffectResistance(), 1)}";
        }
    }
}
