using DotaHeroes.API.Features;

namespace DotaHeroes.API.Items
{
    public class MantleOfIntelligence : AutoItem
    {
        public override string Name => "Mantle Of Intelligence";

        public override string Slug => "mantle_of_intelligence";

        public override string Description => "mantle_of_intelligence";

        public override string Lore => "mantle_of_intelligence is mantle_of_intelligence";

        public MantleOfIntelligence() : base()
        {
        }

        protected MantleOfIntelligence(Hero owner) : base(owner)
        {
        }

        public override Item Create(Hero owner)
        {
            return new MantleOfIntelligence(owner);
        }
    }
}
