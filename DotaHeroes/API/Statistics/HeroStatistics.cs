using DotaHeroes.API;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Interfaces;
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

                var maximumHealth = HealthAndMana.MaximumHealth;
                var healthReg = HealthAndMana.HealthRegeneration;

                var healthFromStrength = Constants.MaximumHealthFromStrength * value;
                var healthRegFromStrength = Constants.HealthRegenerationFromStrength * value;

                HealthAndMana.MaximumHealth = maximumHealth + healthFromStrength;
                HealthAndMana.HealthRegeneration = healthReg + healthRegFromStrength;

                UpdateAttackDamage(value);
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

                var armor = Armor.BaseArmor;
                var attackSpeed = Attack.AttackSpeed;

                var armorFromAgility = Constants.ArmorFromAgility * value;
                var attackSpeedFromAgility = (int)(Constants.AttackSpeedFromAgility * value);

                Armor.BaseArmor = armor + armorFromAgility;
                Attack.AttackSpeed = attackSpeed + attackSpeedFromAgility;

                UpdateAttackDamage(value);
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

                var maximumMana = HealthAndMana.MaximumMana;
                var manaReg = HealthAndMana.ManaRegeneration;

                var maximumManaFromIntelligence = Constants.MaximumManaFromIntelligence * value;
                var manaRegFromIntelligence = Constants.ManaRegenerationFromIntelligence * value;

                HealthAndMana.MaximumMana = maximumMana + maximumManaFromIntelligence;
                HealthAndMana.ManaRegeneration = manaReg + manaRegFromIntelligence;

                UpdateAttackDamage(value);
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
            Attribute = attribute;
            HealthAndMana = healthAndManaStatistics;
            Attack = attackStatistics;
            Armor = armorStatistics;
            Resistance = resistanceStatistics;
            Speed = speedStatistics;
            StrengthFromLevel = strengthFromLevel;
            AgilityFromLevel = agilityFromLevel;
            IntelligenceFromLevel = intelligenceFromLevel;
        }

        public HeroStatistics(AttributeType attribute, float strength, float strengthFromLevel, float agility, float agilityFromLevel, float intelligence, float intelligenceFromLevel, HealthAndManaStatistics healthAndManaStatistics, AttackStatistics attackStatistics, ArmorStatistics armorStatistics, ResistanceStatistics resistanceStatistics, SpeedStatistics speedStatistics)
        {
            Attribute = attribute;
            HealthAndMana = healthAndManaStatistics;
            Attack = attackStatistics;
            Armor = armorStatistics;
            Resistance = resistanceStatistics;
            Speed = speedStatistics;
            Strength = strength;
            Agility = agility;
            Intelligence = intelligence;
            StrengthFromLevel = strengthFromLevel;
            AgilityFromLevel = agilityFromLevel;
            IntelligenceFromLevel = intelligenceFromLevel;
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

        private void UpdateAttackDamage(float value)
        {
            if (Attribute == AttributeType.Intelligence)
            {
                Attack.BaseAttackDamage = (int)value;
            }

            if (Attribute == AttributeType.Universal)
            {
                Attack.BaseAttackDamage = (int)((Strength + Agility + Intelligence) / Constants.UniversalDamage);
            }
        }

        public override string ToString()
        {
            var stringBuilder = StringBuilderPool.Shared.Rent();
            stringBuilder.AppendLine(HealthAndMana.ToString());
            stringBuilder.AppendLine(Attack.ToString());
            stringBuilder.AppendLine(Armor.ToString());
            stringBuilder.AppendLine(Resistance.ToString(Intelligence));
            stringBuilder.AppendLine(Speed.ToString());
            stringBuilder.AppendLine(Attribute.ToString());

            return StringBuilderPool.Shared.ToStringReturn(stringBuilder);
        }
    }
}
