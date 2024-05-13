using DotaHeroes.API.Features;

namespace DotaHeroes.API.Items
{
    public class RingOfProtection : AutoItem
    {
        public override string Name => "Ring of protection";

        public override string Slug => "ring_of_protection";

        public override string Description => "Ring of protection";

        public override string Lore => "Ring of protection";

        public RingOfProtection() : base()
        {

        }

        protected RingOfProtection(Hero owner) : base(owner) { }

        public override Item Create(Hero owner)
        {
            return new RingOfProtection(owner);
        }
    }
}
