using DotaHeroes.API.Features;

namespace DotaHeroes.API.Items
{
    public class HelmOfIronWill : AutoItem
    {
        public override string Name => "Helm of iron will";

        public override string Slug => "helm_of_iron_will";

        public override string Description => "Helm of iron will";

        public override string Lore => "Helm of iron will";

        public HelmOfIronWill() : base()
        {
        }

        protected HelmOfIronWill(Hero owner) : base(owner)
        {
        }

        public override Item Create(Hero owner)
        {
            return new HelmOfIronWill(owner);
        }
    }
}
