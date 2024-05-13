using DotaHeroes.API.Features;

namespace DotaHeroes.API.Items
{
    public class WraithBand : AutoItem
    {
        public override string Name => "Wraith Band";

        public override string Slug => "wraith_band";

        public override string Description => "Wraith band";

        public override string Lore => "Pelmeni";

        public WraithBand() : base()
        {
        }

        protected WraithBand(Hero owner) : base(owner)
        {
        }

        public override Item Create(Hero owner)
        {
            return new WraithBand(owner);
        }
    }
}
