using DotaHeroes.API.Features;

namespace DotaHeroes.API.Items.Recipes
{
    public class BracerRecipe : AutoItem
    {
        public override string Name => "Bracer Recipe";

        public override string Slug => "bracer_recipe";

        public override string Description => "bracer_recipe";

        public override string Lore => "bracer_recipe is bracer_recipe";

        public BracerRecipe() : base() { }

        protected BracerRecipe(Hero owner) : base(owner)
        {
        }

        public override Item Create(Hero owner)
        {
            return new BracerRecipe(owner);
        }
    }
}
