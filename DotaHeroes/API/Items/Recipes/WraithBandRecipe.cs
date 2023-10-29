using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System.Collections.Generic;

namespace DotaHeroes.API.Items.Recipes
{
    public class WraithBandRecipe : AutoItem
    {
        public override string Name => "Wraith Band Recipe";

        public override string Slug => "wraith_band_recipe";

        public override string Description => "wraith_band_recipe";

        public override string Lore => "wraith_band_recipe is wraith_band_recipe";

        public WraithBandRecipe() : base() { }

        protected WraithBandRecipe(Hero owner) : base(owner)
        {
        }

        public override Item Create(Hero owner)
        {
            return new WraithBandRecipe(owner);
        }
    }
}
