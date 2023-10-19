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
    public class EvasionStatistics
    {
        /// <summary>
        /// It is from abilities, items.
        /// </summary>
        public List<IEvasionModifier> EvasionModifiers { get; set; }

        /// <summary>
        /// It is from abilities, items.
        /// </summary>
        public List<IBlindModifier> BlindModifiers { get; set; }

        /// <summary>
        /// It is from effects.
        /// </summary>
        public IAccuracyModifier AccuracyModifier { get; set; }

        public EvasionStatistics()
        {
            EvasionModifiers = new List<IEvasionModifier>();
            BlindModifiers = new List<IBlindModifier>();
        }

        public EvasionStatistics(List<IEvasionModifier> evasionModifiers, List<IBlindModifier> blindModifiers)
        {
            EvasionModifiers = evasionModifiers;
            BlindModifiers = blindModifiers;
        }

        public EvasionStatistics(List<IEvasionModifier> evasionModifiers, List<IBlindModifier> blindModifiers, IAccuracyModifier accuracyModifier)
        {
            EvasionModifiers = evasionModifiers;
            BlindModifiers = blindModifiers;
            AccuracyModifier = accuracyModifier;
        }

        public decimal GetEvasion()
        {
            decimal result = 1;

            foreach (var modifier in EvasionModifiers)
            {
                var value = 1 - modifier.Evasion / 100;
                result *= value;
            }

            if (result == 1)
            {
                return 0;
            }

            return result * 100;
        }

        public decimal GetBlind()
        {
            decimal result = 1;

            foreach (var modifier in BlindModifiers)
            {
                var value = modifier.Blind / 100;
                result += value;
            }

            if (result == 1)
            {
                return 0;
            }
            
            return 1 - result * 100;
        }

        public decimal GetEffectiveEvadeChance()
        {
            return (1 - (1 - GetEvasion()) * (1 - GetBlind())) * 100;
        }

        public bool IsCanHit(IAccuracyModifier accuracyModifier)
        {
            decimal accuracy = 0;

            decimal percentChance = 100;

            if (AccuracyModifier != null)
            {
                accuracy = 1 - AccuracyModifier.Accuracy / 100;
            }

            var finalHitChance = 1 - (1 - GetEffectiveEvadeChance() / 100) * accuracy;
            percentChance = finalHitChance * 100;

            if (percentChance > 95)
            {
                return true;
            }

            var random = UnityEngine.Random.Range(1, 100);

            return random > percentChance;
        }

        public override string ToString()
        {
            return "Evasion: " + GetEvasion().ToString();
        }
    }
}
