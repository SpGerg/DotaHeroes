using DotaHeroes.API;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Interfaces;
using DotaHeroes.API.Modifiers;
using DotaHeroes.API.Statistics;
using Exiled.API.Features;
using NorthwoodLib.Pools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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

                var healthFromStrength = Constants.MaximumHealthFromStrength * value;
                var healthRegFromStrength = Constants.HealthRegenerationFromStrength * value;

                HealthAndMana.MaximumHealth = healthFromStrength + HealthAndMana.BaseHealth;
                HealthAndMana.HealthRegeneration = healthRegFromStrength + HealthAndMana.BaseHealthRegeneration;

                UpdateAttackDamage(value, AttributeType.Strength);
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

                var attackSpeedFromAgility = (int)(Constants.AttackSpeedFromAgility * value);

                Attack.AttackSpeed = attackSpeedFromAgility;

                UpdateAttackDamage(value, AttributeType.Agility);
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

                var maximumManaFromIntelligence = Constants.MaximumManaFromIntelligence * intelligence;
                var manaRegFromIntelligence = Constants.ManaRegenerationFromIntelligence * intelligence;

                HealthAndMana.MaximumMana = maximumManaFromIntelligence + HealthAndMana.BaseMana;
                HealthAndMana.ManaRegeneration = manaRegFromIntelligence + HealthAndMana.BaseManaRegeneration;

                UpdateAttackDamage(value, AttributeType.Intelligence);
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

        // Im init cuz all heroes by default have 0% evasion. wepith eawskughifwean, fmcvzkloppjito 0pwea
        public EvasionStatistics Evasion { get; } = new EvasionStatistics();

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
            Speed = new SpeedStatistics(hero, hero.HeroStatistics.Speed.Speed);
        }

        public HeroStatistics(AttributeType attribute, decimal strength, decimal strengthFromLevel, decimal agility, decimal agilityFromLevel, decimal intelligence, decimal intelligenceFromLevel, HealthAndManaStatistics healthAndManaStatistics, AttackStatistics attackStatistics, ArmorStatistics armorStatistics, ResistanceStatistics resistanceStatistics, SpeedStatistics speedStatistics)
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
            Strength = strength;
            Agility = agility;
            Intelligence = intelligence;
        }

        public HeroStatistics(HeroStatistics heroStatistics, Hero hero)
        {
            AttributeType = heroStatistics.AttributeType;
            HealthAndMana = heroStatistics.HealthAndMana;
            Attack = heroStatistics.Attack;
            Armor = heroStatistics.Armor;
            Resistance = heroStatistics.Resistance;
            Speed = new SpeedStatistics(hero, heroStatistics.Speed.Speed);
            Hero = hero;
            StrengthFromLevel = heroStatistics.StrengthFromLevel;
            AgilityFromLevel = heroStatistics.AgilityFromLevel;
            IntelligenceFromLevel = heroStatistics.IntelligenceFromLevel;
            Strength = heroStatistics.Strength;
            Agility = heroStatistics.Agility;
            Intelligence = heroStatistics.Intelligence;
        }

        public virtual void AddOrReduceStatistics(IReadOnlyDictionary<StatisticsType, Value> statistics, bool isReduce)
        {
            foreach (var value in statistics)
            {
                //im dont know how im can make it better
                switch (value.Key)
                {
                    case StatisticsType.Strength:
                        Strength += GetValue(value.Value.CoolValue, Strength, value.Value.IsPercent, isReduce);
                        break;
                    case StatisticsType.Agility:
                        Agility += GetValue(value.Value.CoolValue, Agility, value.Value.IsPercent, isReduce);
                        break;
                    case StatisticsType.Intelligence:
                        Intelligence += GetValue(value.Value.CoolValue, Intelligence, value.Value.IsPercent, isReduce);
                        break;
                    case StatisticsType.AllAttributes:
                        Strength += GetValue(value.Value.CoolValue, Strength, value.Value.IsPercent, isReduce);
                        Agility += GetValue(value.Value.CoolValue, Agility, value.Value.IsPercent, isReduce);
                        Intelligence += GetValue(value.Value.CoolValue, Intelligence, value.Value.IsPercent, isReduce);
                        break;
                    case StatisticsType.Health:
                        HealthAndMana.Health += GetValue(value.Value.CoolValue, HealthAndMana.Health, value.Value.IsPercent, isReduce);
                        break;
                    case StatisticsType.Mana:
                        HealthAndMana.Mana += GetValue(value.Value.CoolValue, HealthAndMana.Mana, value.Value.IsPercent, isReduce);
                        break;
                    case StatisticsType.HealthRegeneration:
                        HealthAndMana.HealthRegeneration += GetValue(value.Value.CoolValue, HealthAndMana.Health, value.Value.IsPercent, isReduce);
                        break;
                    case StatisticsType.ManaRegeneration:
                        HealthAndMana.ManaRegeneration += GetValue(value.Value.CoolValue, HealthAndMana.Mana, value.Value.IsPercent, isReduce);
                        break;
                    case StatisticsType.ExtraAttackDamage:
                        Attack.ExtraAttackDamage += (int)GetValue(value.Value.CoolValue, Attack.ExtraAttackDamage, value.Value.IsPercent, isReduce);
                        break;
                    case StatisticsType.BaseAttackDamage:
                        Attack.AttackDamage += (int)GetValue(value.Value.CoolValue, Attack.AttackDamage, value.Value.IsPercent, isReduce);
                        break;
                    case StatisticsType.Accuracy:
                        if (Evasion.AccuracyModifier.Accuracy > value.Value.CoolValue) return;
                        Evasion.AccuracyModifier.Accuracy = GetValue(value.Value.CoolValue, Evasion.AccuracyModifier.Accuracy, value.Value.IsPercent, isReduce);
                        break;
                    case StatisticsType.Armor:
                        Armor.ArmorModifiers.Add(new ArmorModifier(
                            GetValue(value.Value.CoolValue, Armor.GetBaseArmor(Agility), value.Value.IsPercent, isReduce)));
                        break;
                    case StatisticsType.Blind:
                        Evasion.BlindModifiers.Add(new BlindModifier(
                            GetValue(value.Value.CoolValue, Evasion.GetBlind(), value.Value.IsPercent, isReduce)));
                        break;
                    case StatisticsType.Evasion:
                        Evasion.EvasionModifiers.Add(new EvasionModifier(
                            GetValue(value.Value.CoolValue, Evasion.GetEvasion(), value.Value.IsPercent, isReduce)));
                        break;
                    case StatisticsType.NegativeArmor:
                        Armor.NegativeArmorModifiers.Add(new NegativeArmorModifier(
                            GetValue(value.Value.CoolValue, Armor.GetNegativeArmorFromModifiers(), value.Value.IsPercent, isReduce)));
                        break;
                    case StatisticsType.MagicResistance:
                        Resistance.ResistanceModifiers.Add(new ResistanceModifier(
                            GetValue(value.Value.CoolValue, Resistance.GetMagicResistance(Intelligence), value.Value.IsPercent, isReduce), 0));
                        break;
                    case StatisticsType.EffectResistance:
                        Resistance.ResistanceModifiers.Add(new ResistanceModifier(0,
                            GetValue(value.Value.CoolValue, Resistance.GetEffectResistance(), value.Value.IsPercent, isReduce)));
                        break;
                }
            }
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
            stringBuilder.AppendLine(Armor.ToString(Agility));
            stringBuilder.AppendLine(Resistance.ToString(Intelligence));
            stringBuilder.AppendLine(Evasion.ToString());
            stringBuilder.AppendLine(Speed.ToString());
            stringBuilder.AppendLine(AttributeType.ToString());

            return StringBuilderPool.Shared.ToStringReturn(stringBuilder);
        }

        private void UpdateAttackDamage(decimal value, AttributeType attributeType)
        {
            if (AttributeType == attributeType)
            {
                Attack.AttackDamage = (int)value + Attack.BaseAttackDamage;
            }

            if (AttributeType == AttributeType.Universal)
            {
                Attack.AttackDamage = (int)((Strength + Agility + Intelligence) / Constants.UniversalDamage) + Attack.BaseAttackDamage;
            }
        }

        private decimal GetValue(decimal value, decimal fullValue, bool isPercent, bool isReduce)
        {
            var total_value = value;

            if (isPercent)
            {
                total_value = (fullValue / 100) * value;
            }

            if (value < 0 && isReduce)
            {
                total_value = -total_value;
            }
            else if (isReduce)
            {
                total_value = -total_value;
            }

            Log.Info(total_value);

            return total_value;
        }
    }
}
