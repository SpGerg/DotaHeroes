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
        public float MagicResistance { get; set; }

        public float EffectResistance { get; set; }

        public List<IResistanceModifier> ResistanceModifiers { get; set; }

        public const float BaseResistance = 25;

        public ResistanceStatistics()
        {
            MagicResistance = BaseResistance;
            EffectResistance = BaseResistance;
            ResistanceModifiers = new List<IResistanceModifier>();
        }

        public ResistanceStatistics(float magicResistance, float baseEffectResistance)
        {
            MagicResistance = magicResistance;
            EffectResistance = baseEffectResistance;
            ResistanceModifiers = new List<IResistanceModifier>();
        }

        public float GetEffectResistance()
        {
            return Mathf.Clamp(1 - (1 - EffectResistance / 10) * GetEffectResistanceFromModifiers(), 0, 99) * 10;
        }

        public float GetEffectDuration(float originalDuration)
        {
            return originalDuration * (100 - GetEffectResistance());
        }

        public float GetMagicResistance(float intelligence)
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

        public string ToString(float intelligence)
        {
            return $"Magic resistance: {GetMagicResistance(intelligence)} Effect resistance: {GetEffectResistance()}";
        }
    }
}
