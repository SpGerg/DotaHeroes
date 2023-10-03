using DotaHeroes.API.Enums;
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
                new HeroStatistics(AttributeType.Strength,
                30,
                3.0m,
                14,
                1.5m,
                16,
                0.5m,
                new HealthAndManaStatistics(10, 75),
                new AttackStatistics(48, 0, 100, 0.6m),
                new ArmorStatistics(),
                new ResistanceStatistics(),
                new SpeedStatistics())
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
                }
            )
            },
        };
    }
}
