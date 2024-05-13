using DotaHeroes.API.Features;

namespace DotaHeroes.API.Items.Recipes
{
    public class BucklerRecipe : AutoItem
    {
        public override string Name => "Buckler recipe";

        public override string Slug => "buckler_recipe";

        public override string Description => "Buckler recipe";

        public override string Lore => "Buckler recipe";

        public BucklerRecipe() : base() { }

        protected BucklerRecipe(Hero owner) : base(owner)
        {
        }

        public override Item Create(Hero owner)
        {
            return new BucklerRecipe(owner);
        }
    }
}
