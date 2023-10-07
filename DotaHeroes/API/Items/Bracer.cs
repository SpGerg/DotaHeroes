using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Items
{
    public class Bracer : Item
    {
        public override IReadOnlyDictionary<StatisticsType, float> Statistics => new Dictionary<StatisticsType, float>()
        {
            { StatisticsType.Strength, 5 },
            { StatisticsType.Agility, 2 },
            { StatisticsType.Intelligence, 2 },
            { StatisticsType.HealthRegeneration, 0.75f },
            { StatisticsType.ExtraAttackDamage, 3 },
        };

        public Bracer(Player owner) : base(owner)
        {
        }
    }
}
