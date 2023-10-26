using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Items
{
    public class Broadsword : Item
    {
        public override string Name => "Broadsword";

        public override string Slug => "broadsword";

        public override string Description => "Broadsword";

        public override string Lore => "Broadsword";

        public override Ability MainAbility { get; } = DTAPI.GetAbilityOrDefaultBySlug(Plugin.Instance.Config.Items["broadsword"].Ability);

        public override IReadOnlyDictionary<StatisticsType, Value> Statistics { get; } = Plugin.Instance.Config.Items["broadsword"].Statistics;

        public override int Cost { get; } = Plugin.Instance.Config.Items["broadsword"].Cost;

        public override int SellCost { get; } = Plugin.Instance.Config.Items["broadsword"].SellCost;

        public Broadsword() { }

        protected Broadsword(Hero owner) : base(owner) { }

        public override Item Create(Hero owner)
        {
            return new Broadsword(owner);
        }
    }
}
