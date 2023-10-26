using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Items.Recipes
{
    public class BucklerRecipe : Item
    {
        public override string Name => "Buckler recipe";

        public override string Slug => "buckler_recipe";

        public override string Description => "Buckler recipe";

        public override string Lore => "Buckler recipe";

        public override Ability MainAbility { get; } = DTAPI.GetAbilityOrDefaultBySlug(Plugin.Instance.Config.Items["buckler_recipe"].Ability);

        public override List<Ability> Passives { get; } = Ability.ToAbilitiesFromStringList(Plugin.Instance.Config.Items["buckler_recipe"].Passives);

        public override IReadOnlyList<Item> Ingredients { get; } = GetItemsFromStringList(Plugin.Instance.Config.Items["buckler_recipe"].Ingredients);

        public override IReadOnlyList<Item> ItemsFromThisItem { get; } = GetItemsFromStringList(Plugin.Instance.Config.Items["buckler_recipe"].ItemsFromThisItems);

        public override IReadOnlyDictionary<StatisticsType, Value> Statistics { get; } = Plugin.Instance.Config.Items["buckler_recipe"].Statistics;

        public override int Cost { get; } = Plugin.Instance.Config.Items["buckler_recipe"].Cost;

        public override int SellCost { get; } = Plugin.Instance.Config.Items["buckler_recipe"].SellCost;

        public BucklerRecipe() { }

        protected BucklerRecipe(Hero owner) : base(owner) { }

        public override Item Create(Hero owner)
        {
            return new BucklerRecipe(owner);
        }
    }
}
