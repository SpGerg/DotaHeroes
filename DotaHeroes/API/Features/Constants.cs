using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Features
{
    public static class Constants
    {
        public const decimal HealthRegenerationFromStrength = 0.1m;
        public const int MaximumHealthFromStrength = 22;
        public const int DamageFromStrength = 1;

        public const decimal ArmorFromAgility = 0.167m;
        public const int AttackSpeedFromAgility = 1;
        public const int DamageFromAgility = 1;

        public const decimal ManaRegenerationFromIntelligence = 0.05m;
        public const float MagicResistanceFromIntelligence = 0.1f;
        public const int MaximumManaFromIntelligence = 12;

        public const decimal UniversalDamage = 0.7m;

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
