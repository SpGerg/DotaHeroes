using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
