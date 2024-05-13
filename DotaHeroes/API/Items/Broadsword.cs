using DotaHeroes.API.Features;

namespace DotaHeroes.API.Items
{
    public class Broadsword : AutoItem
    {
        public override string Name => "Broadsword";

        public override string Slug => "broadsword";

        public override string Description => "Broadsword";

        public override string Lore => "Broadsword";

        public Broadsword() : base() { }

        protected Broadsword(Hero owner) : base(owner)
        {
        }

        public override Item Create(Hero owner)
        {
            return new Broadsword(owner);
        }
    }
}
