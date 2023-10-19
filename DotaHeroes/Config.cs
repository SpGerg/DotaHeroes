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

        [Description("Is sounds using. Very lagy, not recommended.")]
        public bool IsUsingSounds { get; set; }

        [Description("Giving money from kill")]
        public int MoneyFromKill { get; set; } = 100;

        [Description("Giving exp from kill")]
        public int ExpFromKill { get; set; } = 100;

        [Description("Heroes abilities, default hero statistics")]
        public Dictionary<string, HeroSerializable> Heroes { get; set; } = new()
        {
            { "pudge", new HeroSerializable(
                new List<string>
                {
                    "meat_hook",
                    "rot",
                    "flesh_heap",
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
                    BaseAttackRange = 2m,
                    BaseAttackProjectileSpeed = 0,
                    BaseArmor = -1,
                    BaseMagicResistance = ResistanceStatistics.BaseResistance,
                    BaseEffectResistance = 0,
                    BaseSpeed = 35
                }
            )
            },
            { "spirit_breaker", new HeroSerializable(
                new List<string>
                {
                    "charge_of_darkness",
                    "bulldoze",
                    "greater_bash",
                    "nether_strike"
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
                    BaseAttackRange = 2m,
                    BaseAttackProjectileSpeed = 0,
                    BaseArmor = 1,
                    BaseMagicResistance = ResistanceStatistics.BaseResistance,
                    BaseEffectResistance = 0,
                    BaseSpeed = 45
                }
            )
            },
        };

        [Description("Abilties damage, mana cost, cast range and other")]
        public Dictionary<string, AbilitySerializable> Abilites { get; set; } = new()
        {
            { "meat_hook", new AbilitySerializable(
                new Dictionary<string, List<decimal>>
                {
                    { "damage", new List<decimal> { 120, 180, 210, 240 } },
                    { "mana_cost", new List<decimal> { 120, 130, 140, 150 } },
                    { "cooldown", new List<decimal> { 12, 11, 9, 8 } },
                    { "cast_range", new List<decimal> { 20, 30, 40, 50 } }
                }
            )
            },
            { "rot", new AbilitySerializable(
                new Dictionary<string, List<decimal>>
                {
                    { "damage", new List<decimal> { 30, 50, 80, 110 } },
                    { "mana_cost", new List<decimal> { 0, 0, 0, 0 } },
                }
            )
            },
            { "flesh_heap", new AbilitySerializable(
                new Dictionary<string, List<decimal>>
                {
                    { "damage_block", new List<decimal> { 8, 14, 20, 26 } },
                    { "mana_cost", new List<decimal> { 120, 130, 140, 150 } },
                    { "cooldown", new List<decimal> { 12, 11, 9, 8 } },
                }
            )
            },
            { "dismember", new AbilitySerializable(
                new Dictionary<string, List<decimal>>
                {
                    { "damage", new List<decimal>() { 120, 150, 180, 210 } },
                    { "cooldown", new List<decimal>() { 30, 25, 20, 15 } },
                    { "range", new List<decimal>() { 2, 2, 2, 2 } },
                    { "strength_to_damage", new List<decimal>() { 30, 60, 90 } },
                }
            )
            },
            { "charge_of_darkness", new AbilitySerializable(
                new Dictionary<string, List<decimal>>
                {
                    { "extra_speed", new List<decimal> { 25, 40, 55, 70 } },
                    { "stun", new List<decimal> { 1.2m, 1.5m, 1.2m, 2.1m } },
                    { "cooldown", new List<decimal> { 21, 18, 15, 12 } },
                }
            )
            },
            { "bulldoze", new AbilitySerializable(
                new Dictionary<string, List<decimal>>
                {
                    { "duration", new List<decimal> { 8, 8, 8, 8 } },
                    { "extra_effect_resistance", new List<decimal> { 40, 50, 60, 70 } },
                    { "extra_speed", new List<decimal> { 25, 40, 55, 70 } },
                    { "cooldown", new List<decimal> { 22, 20, 18, 16 } },
                }
            )
            },
            { "greater_bash", new AbilitySerializable(
                new Dictionary<string, List<decimal>>
                {
                    { "stun", new List<decimal> { 0.9m, 1.1m, 1.3m, 1.5m } },
                    { "damage_from_speed", new List<decimal> { 25, 30, 35, 40 } },
                    { "chance", new List<decimal> { 17, 17, 17, 17 } },
                    { "cooldown", new List<decimal> { 2, 2, 2 } },
                }
            )
            },
            { "nether_strike", new AbilitySerializable(
                new Dictionary<string, List<decimal>>
                {
                    { "damage", new List<decimal> { 125, 200, 275 } },
                    { "cooldown", new List<decimal> { 90, 70, 50 } },
                }
            )
            }
        };

        [Description("Items damage, mana cost, cast range and other")]
        public Dictionary<string, ItemSerializable> Items { get; set; } = new()
        {
            { "bracer", new ItemSerializable(505, 250, string.Empty, new List<string>(),
                new List<string>()
                {
                    "circlet",
                    "gauntlets_of_strength",
                    "bracer_recipe"
                },
                new List<string>() {},
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
            { "wraith_band", new ItemSerializable(505, 250, string.Empty, new List<string>(),
                new List<string>()
                {
                    "circlet",
                    "slippers_of_agility",
                    "wraith_band_recipe"
                },
                new List<string>() {},
                new Dictionary<StatisticsType, Value>()
                {
                    { StatisticsType.Strength, new Value(2, false) },
                    { StatisticsType.Agility, new Value(5, false) },
                    { StatisticsType.Intelligence, new Value(2, false) },
                    { StatisticsType.Armor, new Value(2, false) },
                    { StatisticsType.AttackSpeed, new Value(5, false) },
                }
            )
            },
            { "null_talisman", new ItemSerializable(505, 250, string.Empty, new List<string>(),
                new List<string>()
                {
                    "circlet",
                    "mantle_of_intelligence",
                    "null_talisman_recipe"
                },
                new List<string>() {},
                new Dictionary<StatisticsType, Value>()
                {
                    { StatisticsType.Strength, new Value(2, false) },
                    { StatisticsType.Agility, new Value(2, false) },
                    { StatisticsType.Intelligence, new Value(5, false) },
                    { StatisticsType.Mana, new Value(3, true) },
                    { StatisticsType.ManaRegeneration, new Value(0.75m, false) },
                }
            )
            },
            { "circlet", new ItemSerializable(155, 77, string.Empty, new List<string>(), new List<string>(),
                new List<string>()
                {
                    "wraith_band",
                    "bracer",
                    "null_talisman"
                },
                new Dictionary<StatisticsType, Value>()
                {
                    { StatisticsType.AllAttributes, new Value(2, false) }
                }
            )
            },
            { "mantle_of_intelligence", new ItemSerializable(140, 70, string.Empty, new List<string>(), new List<string>(),
                new List<string>()
                {
                    "null_talisman"
                },
                new Dictionary<StatisticsType, Value>()
                {
                    { StatisticsType.Intelligence, new Value(3, false) }
                }
            )
            },
            { "slippers_of_agility", new ItemSerializable(140, 70, string.Empty, new List<string>(), new List<string>(),
                new List<string>()
                {
                    "wraith_band"
                },
                new Dictionary<StatisticsType, Value>()
                {
                    { StatisticsType.Agility, new Value(3, false) }
                }
            )
            },
            { "gauntlets_of_strength", new ItemSerializable(140, 70, string.Empty, new List<string>(), new List<string>(),
                new List<string>()
                {
                    "bracer",
                },
                new Dictionary<StatisticsType, Value>()
                {
                    { StatisticsType.Strength, new Value(3, false) }
                }
            )
            },
            { "bracer_recipe", new ItemSerializable(210, 210, string.Empty, new List<string>(), new List<string>(),
                new List<string>()
                {
                    "bracer",
                },
                new Dictionary<StatisticsType, Value>()
                { }
            )
            },
            { "wraith_band_recipe", new ItemSerializable(210, 210, string.Empty, new List<string>(), new List<string>(),
                new List<string>()
                {
                    "wraith_band"
                },
                new Dictionary<StatisticsType, Value>()
                { }
            )
            },
            { "null_talisman_recipe", new ItemSerializable(210, 210, string.Empty, new List<string>(), new List<string>(),
                new List<string>()
                {
                    "null_talisman"
                },
                new Dictionary<StatisticsType, Value>()
                { }
            )
            },
            { "armlet_of_mordiggian", new ItemSerializable(2500, 1250, string.Empty, new List<string>(),
                new List<string>()
                {
                    "helm_of_iron_will",
                    "gloves_of_haste",
                    "blades_of_attack",
                    "armlet_of_mordiggian_recipe"
                },
                new List<string>()
                { },
                new Dictionary<StatisticsType, Value>()
                {
                    { StatisticsType.HealthRegeneration, new Value(5, false) },
                    { StatisticsType.Armor, new Value(6, false) },
                    { StatisticsType.ExtraAttackDamage, new Value(15, false) },
                    { StatisticsType.AttackSpeed, new Value(25, false) }
                }
            )
            },
            { "helm_of_iron_will", new ItemSerializable(975, 487, string.Empty, new List<string>(),
                new List<string>()
                {
                    "armlet_of_mordiggian"
                },
                new List<string>()
                { },
                new Dictionary<StatisticsType, Value>()
                {
                    { StatisticsType.HealthRegeneration, new Value(5, false) },
                    { StatisticsType.Armor, new Value(6, false) },
                }
            )
            },
            { "gloves_of_haste", new ItemSerializable(450, 225, string.Empty, new List<string>(),
                new List<string>()
                {
                    "armlet_of_mordiggian"
                },
                new List<string>()
                { },
                new Dictionary<StatisticsType, Value>()
                {
                    { StatisticsType.AttackSpeed, new Value(20, false) },
                }
            )
            },
            { "blades_of_attack", new ItemSerializable(450, 225, string.Empty, new List<string>(),
                new List<string>()
                {
                    "armlet_of_mordiggian"
                },
                new List<string>()
                { },
                new Dictionary<StatisticsType, Value>()
                {
                    { StatisticsType.ExtraAttackDamage, new Value(9, false) },
                }
            )
            },
            { "armlet_of_mordiggian_recipe", new ItemSerializable(625, 625, string.Empty, new List<string>(),
                new List<string>()
                {
                    "armlet_of_mordiggian"
                },
                new List<string>()
                { },
                new Dictionary<StatisticsType, Value>()
                {
                    { StatisticsType.ExtraAttackDamage, new Value(9, false) },
                }
            )
            },
        };
    }
}
