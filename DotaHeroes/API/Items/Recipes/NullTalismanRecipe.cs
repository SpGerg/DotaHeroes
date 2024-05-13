using DotaHeroes.API.Features;

namespace DotaHeroes.API.Items.Recipes
{
    public class NullTalismanRecipe : AutoItem
    {
        public override string Name => "Null Talisman Recipe";

        public override string Slug => "null_talisman_recipe";

        public override string Description => "null_talisman_recipe";

        public override string Lore => "null_talisman_recipe is null_talisman_recipe";

        public NullTalismanRecipe() : base() { }

        protected NullTalismanRecipe(Hero owner) : base(owner)
        {
        }

        public override Item Create(Hero owner)
        {
            return new NullTalismanRecipe(owner);
        }
    }
}
