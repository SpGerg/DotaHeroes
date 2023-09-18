using DotaHeroes.API;
using DotaHeroes.API.Enums;
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
        public int Strength
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
                    Attack.BaseAttackDamage = value;
                }

                if (Attribute == AttributeType.Universal)
                {
                    Attack.BaseAttackDamage = (int)((Strength + Agility + Intelligence) / Constants.UniversalDamage);
                }
            }
        }

        public int Agility
        {
            get
            {
                return agility;
            }
            set
            {
                agility = value;
                Armor.BaseArmor = Constants.ArmorFromAgility * value;
                Attack.AttackSpeed = Constants.AttackSpeedFromAgility * value;

                if (Attribute == AttributeType.Agility)
                {
                    Attack.BaseAttackDamage = value;
                }

                if (Attribute == AttributeType.Universal)
                {
                    Attack.BaseAttackDamage = (int)((Strength + Agility + Intelligence) / Constants.UniversalDamage);
                }
            }
        }

        public int Intelligence
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
                Resistance.BaseEffectResistance = Constants.MagicResistanceFromIntelligence * value;

                if (Attribute == AttributeType.Intelligence)
                {
                    Attack.BaseAttackDamage = value;
                }

                if (Attribute == AttributeType.Universal)
                {
                    Attack.BaseAttackDamage = (int)((Strength + Agility + Intelligence) / Constants.UniversalDamage);
                }
            }
        }

        public HealthAndManaStatistics HealthAndMana { get; }

        public AttackStatistics Attack { get; }

        public ResistanceStatistics Resistance { get; }

        public ArmorStatistics Armor { get; }

        public SpeedStatistics SpeedStatistics { get; }

        public AttributeType Attribute { get; set; }

        private int strength;

        private int agility;

        private int intelligence;

        public HeroStatistics(AttributeType attribute)
        {
            Attribute = attribute;
            HealthAndMana = new HealthAndManaStatistics();
            Attack = new AttackStatistics();
            Armor = new ArmorStatistics();
            Resistance = new ResistanceStatistics();
            SpeedStatistics = new SpeedStatistics(0);
        }

        public HeroStatistics(AttributeType attribute, HealthAndManaStatistics healthAndManaStatistics, AttackStatistics attackStatistics, ArmorStatistics armorStatistics, ResistanceStatistics resistanceStatistics, SpeedStatistics speedStatistics)
        {
            Attribute = attribute;
            HealthAndMana = healthAndManaStatistics;
            Attack = attackStatistics;
            Armor = armorStatistics;
            Resistance = resistanceStatistics;
            SpeedStatistics = speedStatistics;
        }


        public override string ToString()
        {
            var stringBuilder = StringBuilderPool.Shared.Rent();
            stringBuilder.AppendLine(HealthAndMana.ToString());
            stringBuilder.AppendLine(Attack.ToString());
            stringBuilder.AppendLine(Armor.ToString());
            stringBuilder.AppendLine(Resistance.ToString());
            stringBuilder.AppendLine(SpeedStatistics.ToString());
            stringBuilder.AppendLine(Attribute.ToString());

            return StringBuilderPool.Shared.ToStringReturn(stringBuilder);
        }
    }
}
