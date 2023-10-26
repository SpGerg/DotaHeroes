using CustomPlayerEffects;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Modifiers;
using Exiled.API.Features;
using NorthwoodLib.Pools;
using System.Collections.Generic;

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

        public HeroStatistics(Hero hero, AttributeType attribute)
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
                AddOrReduceStatistic(value.Key, value.Value, isReduce);
            }
        }

        public virtual void AddOrReduceStatistic(StatisticsType statisticsType, Value value, bool isReduce)
        {
            //im dont know how im can make it better
            switch (statisticsType)
            {
                case StatisticsType.Strength:
                    Strength += GetValue(value.CoolValue, Strength, value.IsPercent, isReduce);
                    break;
                case StatisticsType.Agility:
                    Agility += GetValue(value.CoolValue, Agility, value.IsPercent, isReduce);
                    break;
                case StatisticsType.Intelligence:
                    Intelligence += GetValue(value.CoolValue, Intelligence, value.IsPercent, isReduce);
                    break;
                case StatisticsType.AllAttributes:
                    Strength += GetValue(value.CoolValue, Strength, value.IsPercent, isReduce);
                    Agility += GetValue(value.CoolValue, Agility, value.IsPercent, isReduce);
                    Intelligence += GetValue(value.CoolValue, Intelligence, value.IsPercent, isReduce);
                    break;
                case StatisticsType.Health:
                    HealthAndMana.MaximumHealth += GetValue(value.CoolValue, HealthAndMana.MaximumHealth, value.IsPercent, isReduce);
                    break;
                case StatisticsType.Mana:
                    HealthAndMana.MaximumMana += GetValue(value.CoolValue, HealthAndMana.MaximumMana, value.IsPercent, isReduce);
                    break;
                case StatisticsType.HealthRegeneration:
                    HealthAndMana.HealthRegeneration += GetValue(value.CoolValue, HealthAndMana.Health, value.IsPercent, isReduce);
                    break;
                case StatisticsType.ManaRegeneration:
                    HealthAndMana.ManaRegeneration += GetValue(value.CoolValue, HealthAndMana.Mana, value.IsPercent, isReduce);
                    break;
                case StatisticsType.ExtraAttackDamage:
                    Attack.ExtraAttackDamage += (int)GetValue(value.CoolValue, Attack.ExtraAttackDamage, value.IsPercent, isReduce);
                    break;
                case StatisticsType.BaseAttackDamage:
                    Attack.AttackDamage += (int)GetValue(value.CoolValue, Attack.AttackDamage, value.IsPercent, isReduce);
                    break;
                case StatisticsType.Speed:
                    Speed.Speed += (sbyte)GetValue(value.CoolValue, Speed.Speed, value.IsPercent, isReduce);
                    break;
                case StatisticsType.Accuracy:
                    if (Evasion.AccuracyModifier.Accuracy > value.CoolValue) return;
                    Evasion.AccuracyModifier.Accuracy = GetValue(value.CoolValue, Evasion.AccuracyModifier.Accuracy, value.IsPercent, isReduce);
                    break;
                case StatisticsType.Armor:
                    Armor.ArmorModifiers.Add(new ArmorModifier(
                        GetValue(value.CoolValue, Armor.GetBaseArmor(Agility), value.IsPercent, isReduce)));
                    break;
                case StatisticsType.Blind:
                    Evasion.BlindModifiers.Add(new BlindModifier(
                        GetValue(value.CoolValue, Evasion.GetBlind(), value.IsPercent, isReduce)));
                    break;
                case StatisticsType.Evasion:
                    Evasion.EvasionModifiers.Add(new EvasionModifier(
                        GetValue(value.CoolValue, Evasion.GetEvasion(), value.IsPercent, isReduce)));
                    break;
                case StatisticsType.NegativeArmor:
                    Armor.NegativeArmorModifiers.Add(new NegativeArmorModifier(
                        GetValue(value.CoolValue, Armor.GetNegativeArmorFromModifiers(), value.IsPercent, isReduce)));
                    break;
                case StatisticsType.MagicResistance:
                    Resistance.ResistanceModifiers.Add(new ResistanceModifier(
                        GetValue(value.CoolValue, Resistance.GetMagicResistance(Intelligence), value.IsPercent, isReduce), 0));
                    break;
                case StatisticsType.EffectResistance:
                    Resistance.ResistanceModifiers.Add(new ResistanceModifier(0,
                        GetValue(value.CoolValue, Resistance.GetEffectResistance(), value.IsPercent, isReduce)));
                    break;

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

            return total_value;
        }
    }
}
