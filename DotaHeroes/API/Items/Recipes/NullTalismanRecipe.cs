using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Items.Recipes
{
    public class NullTalismanRecipe : Item
    {
        public override string Name => "Null Talisman Recipe";

        public override string Slug => "null_talisman_recipe";

        public override string Description => "null_talisman_recipe";

        public override string Lore => "null_talisman_recipe is null_talisman_recipe";

        public override Ability MainAbility { get; } = API.GetAbilityOrDefaultBySlug("null_talisman_recipe");

        public override List<Ability> Passives { get; } = Ability.ToAbilitiesFromStringList(Plugin.Instance.Config.Items["null_talisman_recipe"].Passives);

        public override IReadOnlyList<Item> Ingredients => GetItemsFromStringList(Plugin.Instance.Config.Items["null_talisman_recipe"].Ingredients);

        public override IReadOnlyList<Item> ItemsFromThisItem => GetItemsFromStringList(Plugin.Instance.Config.Items["null_talisman_recipe"].ItemsFromThisItems);

        public override IReadOnlyDictionary<StatisticsType, Value> Statistics { get; } = Plugin.Instance.Config.Items["null_talisman_recipe"].Statistics;

        public override int Cost { get; } = Plugin.Instance.Config.Items["null_talisman_recipe"].Cost;

        public override int SellCost { get; } = Plugin.Instance.Config.Items["null_talisman_recipe"].SellCost;

        public NullTalismanRecipe() { }

        protected NullTalismanRecipe(Hero owner) : base(owner) { }

        public override Item Create(Hero owner)
        {
            return new NullTalismanRecipe(owner);
        }
    }
}
