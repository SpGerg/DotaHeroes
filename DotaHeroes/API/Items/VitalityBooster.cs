using DotaHeroes.API.Features;

namespace DotaHeroes.API.Items
{
    public class VitalityBooster : AutoItem
    {
        public override string Name => "Vitality booster";

        public override string Slug => "vitality_booster";

        public override string Description => "Vitality booster";

        public override string Lore => "Vitality booster";

        public VitalityBooster() : base()
        {

        }

        protected VitalityBooster(Hero owner) : base(owner)
        {

        }

        public override Item Create(Hero owner)
        {
            return new VitalityBooster(owner);
        }
    }
}
