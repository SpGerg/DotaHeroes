using DotaHeroes.API.Features;

namespace DotaHeroes.API.Items
{
    public class GauntletsOfStrength : AutoItem
    {
        public override string Name => "Gauntlets Of Strength";

        public override string Slug => "gauntlets_of_strength";

        public override string Description => "gauntlets_of_strength";

        public override string Lore => "gauntlets_of_strength is gauntlets_of_strength";

        public GauntletsOfStrength() : base()
        {
        }

        protected GauntletsOfStrength(Hero owner) : base(owner)
        {
        }

        public override Item Create(Hero owner)
        {
            return new GauntletsOfStrength(owner);
        }
    }
}
