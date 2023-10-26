using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System.Collections.Generic;

namespace DotaHeroes.API.Items
{
    public class WraithBand : Item
    {
        public override string Name => "Wraith Band";

        public override string Slug => "wraith_band";

        public override string Description => "Wraith band";

        public override string Lore => "Pelmeni";

        public override Ability MainAbility { get; } = DTAPI.GetAbilityOrDefaultBySlug(Plugin.Instance.Config.Items["wraith_band"].Ability);

        public override List<Ability> Passives { get; } = Ability.ToAbilitiesFromStringList(Plugin.Instance.Config.Items["wraith_band"].Passives);

        public override IReadOnlyList<Item> Ingredients => GetItemsFromStringList(Plugin.Instance.Config.Items["wraith_band"].Ingredients);

        public override IReadOnlyList<Item> ItemsFromThisItem => GetItemsFromStringList(Plugin.Instance.Config.Items["wraith_band"].ItemsFromThisItems);

        public override IReadOnlyDictionary<StatisticsType, Value> Statistics { get; } = Plugin.Instance.Config.Items["wraith_band"].Statistics;

        public override int Cost { get; } = Plugin.Instance.Config.Items["wraith_band"].Cost;

        public override int SellCost { get; } = Plugin.Instance.Config.Items["wraith_band"].SellCost;

        public WraithBand() { }

        protected WraithBand(Hero owner) : base(owner) { }

        public override Item Create(Hero owner)
        {
            return new WraithBand(owner);
        }
    }
}
