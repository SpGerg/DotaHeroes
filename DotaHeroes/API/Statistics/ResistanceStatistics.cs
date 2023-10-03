using DotaHeroes.API.Interfaces;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DotaHeroes.API.Statistics
{
    public class ResistanceStatistics
    {
        public decimal MagicResistance { get; set; }

        public decimal EffectResistance { get; set; }

        public List<IResistanceModifier> ResistanceModifiers { get; set; }

        public const decimal BaseResistance = 25;

        public ResistanceStatistics()
        {
            MagicResistance = BaseResistance;
            EffectResistance = BaseResistance;
            ResistanceModifiers = new List<IResistanceModifier>();
        }

        public ResistanceStatistics(decimal magicResistance, decimal baseEffectResistance)
        {
            MagicResistance = magicResistance;
            EffectResistance = baseEffectResistance;
            ResistanceModifiers = new List<IResistanceModifier>();
        }

        public float GetEffectResistance()
        {
            return Mathf.Clamp(1 - (1 - (float)EffectResistance / 10) * GetEffectResistanceFromModifiers(), 0, 99) * 10;
        }

        public float GetEffectDuration(float originalDuration)
        {
            return originalDuration * (100 - GetEffectResistance());
        }

        public decimal GetMagicResistance(decimal intelligence)
        {
            return MagicResistance + (intelligence / 10);
        }

        private float GetEffectResistanceFromModifiers()
        {
            ResistanceModifiers.Sort();

            float result = 1;

            foreach (var modifier in ResistanceModifiers)
            {
                result *= (1 - modifier.EffectResistance / 10);
            }

            return result;
        }

        public override string ToString()
        {
            return $"Effect resistance: {GetEffectResistance()}";
        }

        public string ToString(decimal intelligence)
        {
            return $"Magic resistance: {GetMagicResistance(intelligence)} Effect resistance: {GetEffectResistance()}";
        }
    }
}
