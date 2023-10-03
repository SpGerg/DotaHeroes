using DotaHeroes.API;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Events.Handlers;
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
        public decimal Strength
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

        public decimal Agility
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

        public decimal Intelligence
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

                HealthAndMana.MaximumMana = (int)(maximumMana + maximumManaFromIntelligence);
                HealthAndMana.ManaRegeneration = (int)(manaReg + manaRegFromIntelligence);

                UpdateAttackDamage(value);
            }
        }

        public decimal StrengthFromLevel { get; }

        public decimal AgilityFromLevel { get; }

        public decimal IntelligenceFromLevel { get; }

        public HealthAndManaStatistics HealthAndMana { get; }

        public AttackStatistics Attack { get; }

        public ResistanceStatistics Resistance { get; }

        public ArmorStatistics Armor { get; }

        public SpeedStatistics Speed { get; }

        public AttributeType AttributeType { get; set; }

        public Features.Hero Hero { get; }

        private decimal strength;

        private decimal agility;

        private decimal intelligence;

        public HeroStatistics()
        {
            AttributeType = AttributeType.None;
            HealthAndMana = new HealthAndManaStatistics();
            Attack = new AttackStatistics();
            Armor = new ArmorStatistics();
            Resistance = new ResistanceStatistics();
            Speed = new SpeedStatistics();
        }

        public HeroStatistics(Features.Hero hero, AttributeType attribute)
        {
            AttributeType = attribute;
            HealthAndMana = new HealthAndManaStatistics();
            Attack = new AttackStatistics();
            Armor = new ArmorStatistics();
            Resistance = new ResistanceStatistics();
            Speed = new SpeedStatistics(hero);
        }

        public HeroStatistics(AttributeType attribute, decimal strengthFromLevel, decimal agilityFromLevel, decimal intelligenceFromLevel, HealthAndManaStatistics healthAndManaStatistics, AttackStatistics attackStatistics, ArmorStatistics armorStatistics, ResistanceStatistics resistanceStatistics, SpeedStatistics speedStatistics)
        {
            AttributeType = attribute;
            HealthAndMana = healthAndManaStatistics;
            Attack = attackStatistics;
            Armor = armorStatistics;
            Resistance = resistanceStatistics;
            Speed = speedStatistics;
            StrengthFromLevel = strengthFromLevel;
            AgilityFromLevel = agilityFromLevel;
            IntelligenceFromLevel = intelligenceFromLevel;
        }

        public HeroStatistics(AttributeType attribute, decimal strength, decimal strengthFromLevel, decimal agility, decimal agilityFromLevel, decimal intelligence, decimal intelligenceFromLevel, HealthAndManaStatistics healthAndManaStatistics, AttackStatistics attackStatistics, ArmorStatistics armorStatistics, ResistanceStatistics resistanceStatistics, SpeedStatistics speedStatistics)
        {
            AttributeType = attribute;
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

        public HeroStatistics(HeroStatistics heroStatistics, Features.Hero hero)
        {
            StrengthFromLevel = heroStatistics.StrengthFromLevel;
            AgilityFromLevel = heroStatistics.AgilityFromLevel;
            IntelligenceFromLevel = heroStatistics.IntelligenceFromLevel;
            AttributeType = heroStatistics.AttributeType;
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

        private void UpdateAttackDamage(decimal value)
        {
            if (AttributeType == AttributeType.Intelligence)
            {
                Attack.BaseAttackDamage = (int)value;
            }

            if (AttributeType == AttributeType.Universal)
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
            stringBuilder.AppendLine(AttributeType.ToString());

            return StringBuilderPool.Shared.ToStringReturn(stringBuilder);
        }
    }
}
