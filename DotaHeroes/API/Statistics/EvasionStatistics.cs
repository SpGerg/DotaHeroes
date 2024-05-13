using DotaHeroes.API.Interfaces;
using System.Collections.Generic;

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

        public double GetEvasion()
        {
            double result = 1;

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

        public double GetBlind()
        {
            double result = 1;

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

        public double GetEffectiveEvadeChance()
        {
            return (1 - (1 - GetEvasion()) * (1 - GetBlind())) * 100;
        }

        public bool IsCanHit(IAccuracyModifier accuracyModifier)
        {
            double accuracy = 0;

            double percentChance = 100;

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
