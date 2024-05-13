using DotaHeroes.API.Features;

namespace DotaHeroes.API.Items
{
    public class Bracer : AutoItem
    {
        public override string Name => "Bracer";

        public override string Slug => "bracer";

        public override string Description => "Bracer";

        public override string Lore => "Bracer";

        public Bracer() : base()
        {
        }

        protected Bracer(Hero owner) : base(owner)
        {
        }

        public override Item Create(Hero owner)
        {
            return new Bracer(owner);
        }
    }
}
