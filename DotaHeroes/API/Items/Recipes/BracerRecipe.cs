using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System.Collections.Generic;

namespace DotaHeroes.API.Items.Recipes
{
    public class BracerRecipe : Item
    {
        public override string Name => "Bracer Recipe";

        public override string Slug => "bracer_recipe";

        public override string Description => "bracer_recipe";

        public override string Lore => "bracer_recipe is bracer_recipe";

        public override Ability MainAbility { get; } = API.GetAbilityOrDefaultBySlug("bracer_recipe");

        public override List<Ability> Passives { get; } = Ability.ToAbilitiesFromStringList(Plugin.Instance.Config.Items["bracer_recipe"].Passives);

        public override IReadOnlyList<Item> Ingredients => GetItemsFromStringList(Plugin.Instance.Config.Items["bracer_recipe"].Ingredients);

        public override IReadOnlyList<Item> ItemsFromThisItem => GetItemsFromStringList(Plugin.Instance.Config.Items["bracer_recipe"].ItemsFromThisItems);

        public override IReadOnlyDictionary<StatisticsType, Value> Statistics { get; } = Plugin.Instance.Config.Items["bracer_recipe"].Statistics;

        public override int Cost { get; } = Plugin.Instance.Config.Items["bracer_recipe"].Cost;

        public override int SellCost { get; } = Plugin.Instance.Config.Items["bracer_recipe"].SellCost;

        public BracerRecipe() { }

        protected BracerRecipe(Hero owner) : base(owner) { }

        public override Item Create(Hero owner)
        {
            return new BracerRecipe(owner);
        }
    }
}
