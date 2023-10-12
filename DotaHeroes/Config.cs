using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Features.Serializables;
using DotaHeroes.API.Statistics;
using Exiled.API.Interfaces;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes
{
    public class Config : IConfig
    {
        [Description("Is enabled or not")]
        public bool IsEnabled { get; set; }

        [Description("Debug or not")]
        public bool Debug { get; set; }

        [Description("Giving money from kill")]
        public int KillFromMoney { get; set; }

        [Description("Giving exp from kill")]
        public int ExpFromMoney { get; set; }

        [Description("Heroes abilities, default hero statistics")]
        public Dictionary<string, HeroSerializable> Heroes { get; set; } = new()
        {
            { "Pudge", new HeroSerializable("Pudge", "Pudge",
                new List<string>
                {
                    "meathook",
                    "rot",
                    "fleshheap",
                    "dismember"
                },
                new List<RoleTypeId>
                {

                },
                HeroClassType.Melee,
                new HeroStatisticsSerializable()
                {
                    Attribute = AttributeType.Strength,
                    Strength = 25,
                    StrengthFromLevel = 3.0m,
                    Agility = 14,
                    AgilityFromLevel = 1.4m,
                    Intelligence = 16,
                    IntelligenceFromLevel = 1.8m,
                    BaseHealth = 120,
                    BaseHealthRegeneration = 1.75m,
                    BaseMana = 75,
                    BaseAttackDamage = 45,
                    BaseAttackSpeed = 10,
                    BaseAttackRange = 0.6m,
                    BaseAttackProjectileSpeed = 0,
                    BaseArmor = -1,
                    BaseMagicResistance = ResistanceStatistics.BaseResistance,
                    BaseEffectResistance = ResistanceStatistics.BaseResistance,
                    BaseSpeed = 0
                }
            )
            },
            { "Spirit breaker", new HeroSerializable("Spirit breaker", "Spirit breaker",
                new List<string>
                {
                    "meathook",
                },
                new List<RoleTypeId>
                {

                },
                HeroClassType.Melee,
                new HeroStatisticsSerializable()
                {
                    Attribute = AttributeType.Strength,
                    Strength = 28,
                    StrengthFromLevel = 3.3m,
                    Agility = 17,
                    AgilityFromLevel = 1.7m,
                    Intelligence = 14,
                    IntelligenceFromLevel = 1.8m,
                    BaseHealth = 120,
                    BaseHealthRegeneration = 1.25m,
                    BaseMana = 75,
                    BaseManaRegeneration = 0.5m,
                    BaseAttackDamage = 31,
                    BaseAttackSpeed = 10,
                    BaseAttackRange = 0.6m,
                    BaseAttackProjectileSpeed = 0,
                    BaseArmor = 1,
                    BaseMagicResistance = ResistanceStatistics.BaseResistance,
                    BaseEffectResistance = ResistanceStatistics.BaseResistance,
                    BaseSpeed = 0
                }
            )
            },
        };

        [Description("Abilties damage, mana cost, cast range and other")]
        public Dictionary<string, AbilitySerializable> Abilites { get; set; } = new()
        {
            { "meathook", new AbilitySerializable("Meat hook", "Meat hook",
                new Dictionary<string, List<float>>
                {
                    { "damage", new List<float> { 120, 180, 210, 240 } },
                    { "mana_cost", new List<float> { 120, 130, 140, 150 } },
                    { "cooldown", new List<float> { 12, 11, 9, 8 } },
                    { "cast_range", new List<float> { 20, 30, 40, 50 } }
                }
            )
            },
            { "rot", new AbilitySerializable("Rot", "Rot",
                new Dictionary<string, List<float>>
                {
                    { "damage", new List<float> { 30, 50, 80, 110 } },
                    { "mana_cost", new List<float> { 0, 0, 0, 0 } },
                }
            )
            },
            { "fleshheap", new AbilitySerializable("Flesh heap", "Flesh heap",
                new Dictionary<string, List<float>>
                {
                    { "damage_block", new List<float> { 8, 14, 20, 26 } },
                    { "mana_cost", new List<float> { 120, 130, 140, 150 } },
                    { "cooldown", new List<float> { 12, 11, 9, 8 } },
                }
            )
            },
            { "dismember", new AbilitySerializable("Dismember", "Dismember",
                new Dictionary<string, List<float>>
                {
                    { "damage", new List<float>() { 120, 150, 180, 210 } },
                    { "cooldown", new List<float>() { 30, 25, 20, 15 } },
                    { "range", new List<float>() { 2, 2, 2, 2 } },
                    { "strength_to_damage", new List<float>() { 30, 60, 90 } },
                }
            )
            },
        };

        [Description("Items damage, mana cost, cast range and other")]
        public Dictionary<string, ItemSerializable> Items { get; set; } = new()
        {
            { "bracer", new ItemSerializable("Bracer", "Bracer description", "Bracer lore", string.Empty,
                new Dictionary<StatisticsType, Value>()
                {
                    { StatisticsType.Strength, new Value(5, false) },
                    { StatisticsType.Agility, new Value(2, false) },
                    { StatisticsType.Intelligence, new Value(2, false) },
                    { StatisticsType.HealthRegeneration, new Value(0.75m, false) },
                    { StatisticsType.ExtraAttackDamage, new Value(3, false) },
                }
            )
            },
        };
    }
}
