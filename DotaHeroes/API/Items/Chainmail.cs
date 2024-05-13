using DotaHeroes.API.Features;

namespace DotaHeroes.API.Items
{
    public class Chainmail : AutoItem
    {
        public override string Name => "Chainmail";

        public override string Slug => "chainmail";

        public override string Description => "Chainmail";

        public override string Lore => "Chainmail";

        public Chainmail() : base() { }

        public Chainmail(Hero owner) : base(owner)
        {
        }

        public override Item Create(Hero owner)
        {
            return new Chainmail(owner);
        }
    }
}
