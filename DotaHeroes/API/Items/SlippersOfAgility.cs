using DotaHeroes.API.Features;

namespace DotaHeroes.API.Items
{
    public class SlippersOfAgility : AutoItem
    {
        public override string Name => "Slippers Of Agility";

        public override string Slug => "slippers_of_agility";

        public override string Description => "slippers_of_agility";

        public override string Lore => "slippers_of_agility is slippers_of_agility";

        public SlippersOfAgility() : base()
        {
        }

        protected SlippersOfAgility(Hero owner) : base(owner)
        {
        }

        public override Item Create(Hero owner)
        {
            return new SlippersOfAgility(owner);
        }
    }
}
