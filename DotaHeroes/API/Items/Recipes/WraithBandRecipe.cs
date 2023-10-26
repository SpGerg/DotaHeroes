using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System.Collections.Generic;

namespace DotaHeroes.API.Items.Recipes
{
    public class WraithBandRecipe : Item
    {
        public override string Name => "Wraith Band Recipe";

        public override string Slug => "wraith_band_recipe";

        public override string Description => "wraith_band_recipe";

        public override string Lore => "wraith_band_recipe is wraith_band_recipe";

        public override Ability MainAbility { get; } = DTAPI.GetAbilityOrDefaultBySlug("wraith_band_recipe");

        public override List<Ability> Passives { get; } = Ability.ToAbilitiesFromStringList(Plugin.Instance.Config.Items["wraith_band_recipe"].Passives);

        public override IReadOnlyList<Item> Ingredients => GetItemsFromStringList(Plugin.Instance.Config.Items["wraith_band_recipe"].Ingredients);

        public override IReadOnlyList<Item> ItemsFromThisItem => GetItemsFromStringList(Plugin.Instance.Config.Items["wraith_band_recipe"].ItemsFromThisItems);

        public override IReadOnlyDictionary<StatisticsType, Value> Statistics { get; } = Plugin.Instance.Config.Items["wraith_band_recipe"].Statistics;

        public override int Cost { get; } = Plugin.Instance.Config.Items["wraith_band_recipe"].Cost;

        public override int SellCost { get; } = Plugin.Instance.Config.Items["wraith_band_recipe"].SellCost;

        public WraithBandRecipe() { }

        protected WraithBandRecipe(Hero owner) : base(owner) { }

        public override Item Create(Hero owner)
        {
            return new WraithBandRecipe(owner);
        }
    }
}
