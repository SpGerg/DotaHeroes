using DotaHeroes.API.Features;

namespace DotaHeroes.API.Items.Recipes
{
    public class ArmletOfMordiggianRecipe : AutoItem
    {
        public override string Name => "Armlet of mordiggian recipe";

        public override string Slug => "armlet_of_mordiggian_recipe";

        public override string Description => "Armlet of mordiggian recipe";

        public override string Lore => "Armlet of mordiggian recipe";

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
