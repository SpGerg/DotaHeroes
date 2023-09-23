using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Features
{
    public static class Constants
    {
        public const float HealthRegenerationFromStrength = 0.1f;
        public const int MaximumHealthFromStrength = 22;
        public const int DamageFromStrength = 1;

        public const float ArmorFromAgility = 0.167f;
        public const int AttackSpeedFromAgility = 1;
        public const int DamageFromAgility = 1;

        public const float ManaRegenerationFromIntelligence = 0.05f;
        public const float MagicResistanceFromIntelligence = 0.1f;
        public const int MaximumManaFromIntelligence = 12;

        public const float UniversalDamage = 0.7f;
    }
}
