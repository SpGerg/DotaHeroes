using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Items
{
    public class Vanguard : Item
    {
        public override string Name => "Vanguard";

        public override string Slug => "vanguard";

        public override string Description => "Vanguard";

        public override string Lore => "Vanguard";

        public override Ability MainAbility { get; } = DTAPI.GetAbilityOrDefaultBySlug(Plugin.Instance.Config.Items["vanguard"].Ability);

        public override IReadOnlyList<Item> Ingredients { get; } = GetItemsFromStringList(Plugin.Instance.Config.Items["vanguard"].Ingredients);

        public override IReadOnlyList<Item> ItemsFromThisItem { get; } = GetItemsFromStringList(Plugin.Instance.Config.Items["vanguard"].ItemsFromThisItems);

        public override IReadOnlyDictionary<StatisticsType, Value> Statistics { get; } = Plugin.Instance.Config.Items["vanguard"].Statistics;

        public override int Cost { get; } = Plugin.Instance.Config.Items["vanguard"].Cost;

        public override int SellCost { get; } = Plugin.Instance.Config.Items["vanguard"].SellCost;

        public Vanguard() { }

        protected Vanguard(Hero owner) : base(owner) { }

        public override Item Create(Hero owner)
        {
            return new Vanguard(owner);
        }
    }
}
