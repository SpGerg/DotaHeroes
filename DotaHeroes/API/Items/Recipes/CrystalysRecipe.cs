using DotaHeroes.API.Features;

namespace DotaHeroes.API.Items.Recipes
{
    public class CrystalysRecipe : AutoItem
    {
        public override string Name => "Crystalys recipe";

        public override string Slug => "crystalys_recipe";

        public override string Description => "Crystalys recipe";

        public override string Lore => "Crystalys recipe";

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
