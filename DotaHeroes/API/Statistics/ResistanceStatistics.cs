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

        public float BaseEffectResistance { get; set; }

        public List<float> ResistanceModifiers { get; set; }

        public ResistanceStatistics() { }

        public ResistanceStatistics(float magicResistance, float baseEffectResistance)
        {
            MagicResistance = magicResistance;
            BaseEffectResistance = baseEffectResistance;
            ResistanceModifiers = new List<float>();
        }

        public float GetEffectResistance()
        {
            return Mathf.Clamp(1 - (1 - BaseEffectResistance / 10) * GetEffectResistanceFromModifiers(), 0, 99);
        }

        private float GetEffectResistanceFromModifiers()
        {
            ResistanceModifiers.Sort();

            float result = 0;

            foreach (var modifier in ResistanceModifiers)
            {
                result *= (1 - modifier / 10);
            }

            return result;
        }

        public override string ToString()
        {
            return $"Magic resistance: {MagicResistance} Effect resistance: {GetEffectResistance()}";
        }
    }
}
