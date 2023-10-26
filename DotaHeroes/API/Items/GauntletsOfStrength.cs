using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System.Collections.Generic;

namespace DotaHeroes.API.Items
{
    public class GauntletsOfStrength : Item
    {
        public override string Name => "Gauntlets Of Strength";

        public override string Slug => "gauntlets_of_strength";

        public override string Description => "gauntlets_of_strength";

        public override string Lore => "gauntlets_of_strength is gauntlets_of_strength";

        public override Ability MainAbility { get; } = DTAPI.GetAbilityOrDefaultBySlug(Plugin.Instance.Config.Items["gauntlets_of_strength"].Ability);

        public override List<Ability> Passives { get; } = Ability.ToAbilitiesFromStringList(Plugin.Instance.Config.Items["gauntlets_of_strength"].Passives);

        public override IReadOnlyList<Item> Ingredients => GetItemsFromStringList(Plugin.Instance.Config.Items["gauntlets_of_strength"].Ingredients);

        public override IReadOnlyList<Item> ItemsFromThisItem => GetItemsFromStringList(Plugin.Instance.Config.Items["gauntlets_of_strength"].ItemsFromThisItems);

        public override IReadOnlyDictionary<StatisticsType, Value> Statistics { get; } = Plugin.Instance.Config.Items["gauntlets_of_strength"].Statistics;

        public override int Cost { get; } = Plugin.Instance.Config.Items["gauntlets_of_strength"].Cost;

        public override int SellCost { get; } = Plugin.Instance.Config.Items["gauntlets_of_strength"].SellCost;

        public GauntletsOfStrength() { }

        protected GauntletsOfStrength(Hero owner) : base(owner) { }

        public override Item Create(Hero owner)
        {
            return new GauntletsOfStrength(owner);
        }
    }
}
