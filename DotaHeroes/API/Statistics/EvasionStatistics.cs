using DotaHeroes.API.Interfaces;
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
            decimal result = 0;

            foreach (var modifier in EvasionModifiers)
            {
                var value = 1 - modifier.Evasion / 100;
                result *= value;
            }

            return result * 100;
        }

        public decimal GetBlind()
        {
            decimal result = 0;

            foreach (var modifier in BlindModifiers)
            {
                var value = modifier.Blind / 100;
                result += value;
            }
            
            return 1 - result * 100;
        }

        public decimal GetEffectiveEvadeChance()
        {
            return 1 - GetEvasion() * GetBlind() * 100;
        }

        public bool IsCanHit(IAccuracyModifier accuracyModifier)
        {
            decimal accuracy = 0;

            if (AccuracyModifier != null)
            {
                accuracy = AccuracyModifier.Accuracy;
            }

            var finalHitChance = (accuracy / 100) + (1 - (accuracy / 100)) * (GetEffectiveEvadeChance() / 100);
            var percentChance = finalHitChance * 100;

            var random = UnityEngine.Random.Range(1, 100);

            return random > percentChance;
        }

        public override string ToString()
        {
            return "Evasion: " + GetEvasion().ToString();
        }
    }
}
