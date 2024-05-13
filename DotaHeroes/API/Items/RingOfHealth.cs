using DotaHeroes.API.Features;

namespace DotaHeroes.API.Items
{
    public class RingOfHealth : AutoItem
    {
        public override string Name => "Ring of health";

        public override string Slug => "ring_of_health";

        public override string Description => "Ring of health";

        public override string Lore => "Ring of health";

        public RingOfHealth() : base()
        {
        }

        protected RingOfHealth(Hero owner) : base(owner)
        {
        }

        public override Item Create(Hero owner)
        {
            return new RingOfHealth(owner);
        }
    }
}
