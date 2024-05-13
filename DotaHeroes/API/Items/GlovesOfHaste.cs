using DotaHeroes.API.Features;

namespace DotaHeroes.API.Items
{
    public class GlovesOfHaste : AutoItem
    {
        public override string Name => "Gloves of haste";

        public override string Slug => "gloves_of_haste";

        public override string Description => "Gloves of haste";

        public override string Lore => "Gloves of haste";

        public GlovesOfHaste() : base()
        {
        }

        protected GlovesOfHaste(Hero owner) : base(owner)
        {
        }

        public override Item Create(Hero owner)
        {
            return new GlovesOfHaste(owner);
        }
    }
}
