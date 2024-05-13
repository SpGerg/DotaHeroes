using DotaHeroes.API.Features;

namespace DotaHeroes.API.Items
{
    public class Crystalys : AutoItem
    {
        public override string Name => "Crystalys";

        public override string Slug => "crystalys";

        public override string Description => "Crystalys";

        public override string Lore => "Crystalys";

        public Crystalys() : base()
        {
        }

        protected Crystalys(Hero owner) : base(owner)
        {
        }

        public override Item Create(Hero owner)
        {
            return new Crystalys(owner);
        }
    }
}
