using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Items.Recipes
{
    public class ArmletOfMordiggianRecipe : Item
    {
        public override string Name => "Armlet of mordiggian recipe";

        public override string Slug => "armlet_of_mordiggian_recipe";

        public override string Description => "Armlet of mordiggian recipe";

        public override string Lore => "Armlet of mordiggian recipe";

        public override Ability MainAbility { get; } = API.GetAbilityOrDefaultBySlug("armlet_of_mordiggian_recipe");

        public override List<Ability> Passives { get; } = Ability.ToAbilitiesFromStringList(Plugin.Instance.Config.Items["armlet_of_mordiggian_recipe"].Passives);

        public override IReadOnlyList<Item> Ingredients => GetItemsFromStringList(Plugin.Instance.Config.Items["armlet_of_mordiggian_recipe"].Ingredients);

        public override IReadOnlyList<Item> ItemsFromThisItem => GetItemsFromStringList(Plugin.Instance.Config.Items["armlet_of_mordiggian_recipe"].ItemsFromThisItems);

        public override IReadOnlyDictionary<StatisticsType, Value> Statistics { get; } = Plugin.Instance.Config.Items["helm_of_iron_will"].Statistics;

        public override int Cost { get; } = Plugin.Instance.Config.Items["armlet_of_mordiggian_recipe"].Cost;

        public override int SellCost { get; } = Plugin.Instance.Config.Items["armlet_of_mordiggian_recipe"].SellCost;

        public ArmletOfMordiggianRecipe() : base()
        {
        }

        protected ArmletOfMordiggianRecipe(Hero owner) : base(owner)
        {
        }

        public override Item Create(Hero owner)
        {
            return new ArmletOfMordiggianRecipe(owner);
        }
    }
}
