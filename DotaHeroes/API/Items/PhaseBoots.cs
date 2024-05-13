using DotaHeroes.API.Features;

namespace DotaHeroes.API.Items
{
    public class PhaseBoots : AutoItem
    {
        public override string Name => "Phase boots";

        public override string Slug => "phase_boots";

        public override string Description => "Phase boots";

        public override string Lore => "Kialidl lol kok ik l";

        public PhaseBoots() : base()
        {
        }

        protected PhaseBoots(Hero owner) : base(owner)
        {
        }

        public override Item Create(Hero owner)
        {
            return new PhaseBoots(owner);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
