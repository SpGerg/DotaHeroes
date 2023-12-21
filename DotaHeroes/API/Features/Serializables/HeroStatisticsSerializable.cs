using DotaHeroes.API.Enums;
using DotaHeroes.API.Statistics;

namespace DotaHeroes.API.Features.Serializables
{
    public class HeroStatisticsSerializable
    {
        public double Strength { get; set; }

        public double StrengthFromLevel { get; set; }

        public double Agility { get; set; }

        public double AgilityFromLevel { get; set; }

        public double Intelligence { get; set; }

        public double IntelligenceFromLevel { get; set; }

        public double BaseHealth { get; set; }

        public double BaseHealthRegeneration { get; set; }

        public double BaseMana { get; set; }

        public double BaseManaRegeneration { get; set; }

        public int BaseAttackDamage { get; set; }

        public int BaseAttackSpeed { get; set; }

        public double BaseAttackRange { get; set; }

        public double BaseAttackProjectileSpeed { get; set; }

        public double BaseArmor { get; set; }

        public double BaseMagicResistance { get; set; }

        public double BaseEffectResistance { get; set; }

        public sbyte BaseSpeed { get; set; }

        public AttributeType Attribute { get; set; }

        public HeroStatisticsSerializable() { }

        public HeroStatistics ToHeroStatistics(Hero hero)
        {
            return new HeroStatistics(
                Attribute, Strength, StrengthFromLevel, Agility, AgilityFromLevel, Intelligence, IntelligenceFromLevel,
                new HealthAndManaStatistics(BaseHealth, BaseMana, BaseHealth, BaseMana, BaseHealthRegeneration, BaseManaRegeneration),
                new AttackStatistics(BaseAttackDamage, 0, BaseAttackSpeed, BaseAttackRange, BaseAttackProjectileSpeed),
                new ArmorStatistics(BaseArmor),
                new ResistanceStatistics(BaseMagicResistance, BaseEffectResistance),
                new SpeedStatistics(hero, BaseSpeed)
                );
        }
    }
}
