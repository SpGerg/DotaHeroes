using System.Collections.Generic;

namespace DotaHeroes.API.Features
{
    public static class Constants
    {
        public const double HealthRegenerationFromStrength = 0.1;
        public const int MaximumHealthFromStrength = 22;
        public const int DamageFromStrength = 1;

        public const double ArmorFromAgility = 0.167;
        public const int AttackSpeedFromAgility = 1;
        public const int DamageFromAgility = 1;

        public const double ManaRegenerationFromIntelligence = 0.05;
        public const float MagicResistanceFromIntelligence = 0.1f;
        public const int MaximumManaFromIntelligence = 12;

        public const double UniversalDamage = 0.7;

        public static IReadOnlyDictionary<int, int> ExperienceToLevelUp => new Dictionary<int, int>()
        {
            { 1, 240 },
            { 2, 400 },
            { 3, 520 },
            { 4, 600 },
            { 5, 680 },
            { 6, 760 },
            { 7, 800 },
            { 8, 900 },
            { 9, 1000 },
            { 10, 1100 },
            { 11, 1200 },
            { 12, 1300 },
            { 13, 1400 },
            { 14, 1500 },
            { 15, 1600 },
            { 16, 1700 },
            { 17, 1800 },
            { 18, 1900 },
            { 19, 2000 },
            { 20, 2200 },
            { 21, 2400 },
            { 22, 2600 },
            { 23, 2800 },
            { 24, 3000 },
            { 25, 4000 },
            { 26, 5000 },
            { 27, 6000 },
            { 28, 7000 },
            { 29, 7500 },
            { 30, 7500 },
        };
    }
}
