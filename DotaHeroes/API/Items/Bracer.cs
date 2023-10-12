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
        public override string Name => "Bracer";

        public override string Description => "Bracer";

        public override string Lore => "Bracer";

        public override IReadOnlyDictionary<StatisticsType, Value> Statistics => Plugin.Instance.Config.Items["bracer"].Statistics;

        public Bracer(Player owner) : base(owner)
        {
        }
    }
}
