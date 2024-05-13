using DotaHeroes.API.Features;

namespace DotaHeroes.API.Items
{
    public class BladesOfAttack : AutoItem
    {
        public override string Name => "Blades of attack";

        public override string Slug => "blades_of_attack";

        public override string Description => "Blades of attack";

        public override string Lore => "Blades of attack";

        public BladesOfAttack() : base()
        {
        }

        protected BladesOfAttack(Hero owner) : base(owner)
        {
        }

        public override Item Create(Hero owner)
        {
            return new BladesOfAttack(owner);
        }
    }
}
