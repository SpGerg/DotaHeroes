using DotaHeroes.API.Enums;
using DotaHeroes.API.Statistics;

namespace DotaHeroes.API.Features.Serializables
{
    public class HeroStatisticsSerializable
    {
        public decimal Strength { get; set; }

        public decimal StrengthFromLevel { get; set; }

        public decimal Agility { get; set; }

        public decimal AgilityFromLevel { get; set; }

        public decimal Intelligence { get; set; }

        public decimal IntelligenceFromLevel { get; set; }

        public decimal BaseHealth { get; set; }

        public decimal BaseHealthRegeneration { get; set; }

        public decimal BaseMana { get; set; }

        public decimal BaseManaRegeneration { get; set; }

        public int BaseAttackDamage { get; set; }

        public int BaseAttackSpeed { get; set; }

        public decimal BaseAttackRange { get; set; }

        public decimal BaseAttackProjectileSpeed { get; set; }

        public decimal BaseArmor { get; set; }

        public decimal BaseMagicResistance { get; set; }

        public decimal BaseEffectResistance { get; set; }

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
