using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Items.Recipes
{
    public class CrystalysRecipe : Item
    {
        public override string Name => "Crystalys recipe";

        public override string Slug => "crystalys_recipe";

        public override string Description => "Crystalys recipe";

        public override string Lore => "Crystalys recipe";

        public override Ability MainAbility { get; } = DTAPI.GetAbilityOrDefaultBySlug("crystalys_recipe");

        public override List<Ability> Passives { get; } = Ability.ToAbilitiesFromStringList(Plugin.Instance.Config.Items["crystalys_recipe"].Passives);

        public override IReadOnlyList<Item> Ingredients { get; } = GetItemsFromStringList(Plugin.Instance.Config.Items["crystalys_recipe"].Ingredients);

        public override IReadOnlyList<Item> ItemsFromThisItem { get; } = GetItemsFromStringList(Plugin.Instance.Config.Items["crystalys_recipe"].ItemsFromThisItems);

        public override IReadOnlyDictionary<StatisticsType, Value> Statistics { get; } = Plugin.Instance.Config.Items["crystalys_recipe"].Statistics;

        public override int Cost { get; } = Plugin.Instance.Config.Items["crystalys_recipe"].Cost;

        public override int SellCost { get; } = Plugin.Instance.Config.Items["crystalys_recipe"].SellCost;

        public CrystalysRecipe() : base()
        {
        }

        protected CrystalysRecipe(Hero owner) : base(owner)
        {
        }

        public override Item Create(Hero owner)
        {
            return new CrystalysRecipe(owner);
        }
    }
}
