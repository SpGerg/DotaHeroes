using DotaHeroes.API;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Statistics;
using Exiled.API.Features;
using NorthwoodLib.Pools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Statistics
{
    public class HeroStatistics
    {
        public float Strength
        {
            get
            {
                return strength;
            }
            set
            {
                strength = value;
                HealthAndMana.MaximumHealth = Constants.MaximumHealthFromStrength * value;
                HealthAndMana.HealthRegeneration = Constants.HealthRegenerationFromStrength * value;
                
                if (Attribute == AttributeType.Strength)
                {
                    Attack.BaseAttackDamage = (int)value;
                }

                if (Attribute == AttributeType.Universal)
                {
                    Attack.BaseAttackDamage = (int)((Strength + Agility + Intelligence) / Constants.UniversalDamage);
                }
            }
        }

        public float Agility
        {
            get
            {
                return agility;
            }
            set
            {
                agility = value;
                Armor.BaseArmor = Constants.ArmorFromAgility * value;
                Attack.AttackSpeed = (int)(Constants.AttackSpeedFromAgility * value);

                if (Attribute == AttributeType.Agility)
                {
                    Attack.BaseAttackDamage = (int)value;
                }

                if (Attribute == AttributeType.Universal)
                {
                    Attack.BaseAttackDamage = (int)((Strength + Agility + Intelligence) / Constants.UniversalDamage);
                }
            }
        }

        public float Intelligence
        {
            get
            {
                return intelligence;
            }
            set
            {
                intelligence = value;
                HealthAndMana.MaximumMana = Constants.MaximumManaFromIntelligence * value;
                HealthAndMana.ManaRegeneration = Constants.ManaRegenerationFromIntelligence * value;
                Resistance.EffectResistance = Constants.MagicResistanceFromIntelligence * value;

                if (Attribute == AttributeType.Intelligence)
                {
                    Attack.BaseAttackDamage = (int)value;
                }

                if (Attribute == AttributeType.Universal)
                {
                    Attack.BaseAttackDamage = (int)((Strength + Agility + Intelligence) / Constants.UniversalDamage);
                }
            }
        }

        public float StrengthFromLevel { get; }

        public float AgilityFromLevel { get; }

        public float IntelligenceFromLevel { get; }

        public HealthAndManaStatistics HealthAndMana { get; }

        public AttackStatistics Attack { get; }

        public ResistanceStatistics Resistance { get; }

        public ArmorStatistics Armor { get; }

        public SpeedStatistics Speed { get; }

        public AttributeType Attribute { get; set; }

        public Hero Hero { get; }

        private float strength;

        private float agility;

        private float intelligence;

        public HeroStatistics(Hero hero, AttributeType attribute)
        {
            Attribute = attribute;
            HealthAndMana = new HealthAndManaStatistics();
            Attack = new AttackStatistics();
            Armor = new ArmorStatistics();
            Resistance = new ResistanceStatistics();
            Speed = new SpeedStatistics(hero, 0);
        }

        public HeroStatistics(AttributeType attribute, float strengthFromLevel, float agilityFromLevel, float intelligenceFromLevel, HealthAndManaStatistics healthAndManaStatistics, AttackStatistics attackStatistics, ArmorStatistics armorStatistics, ResistanceStatistics resistanceStatistics, SpeedStatistics speedStatistics)
        {
            StrengthFromLevel = strengthFromLevel;
            AgilityFromLevel = agilityFromLevel;
            IntelligenceFromLevel = intelligenceFromLevel;
            Attribute = attribute;
            HealthAndMana = healthAndManaStatistics;
            Attack = attackStatistics;
            Armor = armorStatistics;
            Resistance = resistanceStatistics;
            Speed = speedStatistics;
        }

        public HeroStatistics(HeroStatistics heroStatistics, Hero hero)
        {
            StrengthFromLevel = heroStatistics.StrengthFromLevel;
            AgilityFromLevel = heroStatistics.AgilityFromLevel;
            IntelligenceFromLevel = heroStatistics.IntelligenceFromLevel;
            Attribute = heroStatistics.Attribute;
            HealthAndMana = heroStatistics.HealthAndMana;
            Attack = heroStatistics.Attack;
            Armor = heroStatistics.Armor;
            Resistance = heroStatistics.Resistance;
            Speed = heroStatistics.Speed;
            Hero = hero;
        }

        public void LevelUp()
        {
            Strength += StrengthFromLevel;
            Agility += AgilityFromLevel;
            Intelligence += IntelligenceFromLevel;
        }

        public override string ToString()
        {
            var stringBuilder = StringBuilderPool.Shared.Rent();
            stringBuilder.AppendLine(HealthAndMana.ToString());
            stringBuilder.AppendLine(Attack.ToString());
            stringBuilder.AppendLine(Armor.ToString());
            stringBuilder.AppendLine(Resistance.ToString());
            stringBuilder.AppendLine(Speed.ToString());
            stringBuilder.AppendLine(Attribute.ToString());

            return StringBuilderPool.Shared.ToStringReturn(stringBuilder);
        }
    }
}
