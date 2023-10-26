using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System.Linq;

namespace DotaHeroes.API.Effects.Pudge
{
    public class FleshHeap : Effect
    {
        public override string Name => "Flesh heap";

        public override string Slug => "flesh_heap";

        public override string Description { get; protected set; } = "Flesh heap";

        public override EffectClassType EffectClassType => EffectClassType.Positive;

        public override int Stack {
            get
            {
                return count;
            }
            set
            {
                count = value;

                Hero.HeroStatistics.Strength -= GivenStrength;
                Hero.HeroStatistics.Strength += StrengthToGive * count;
                GivenStrength += StrengthToGive;
            }
        }

        public int StrengthToGive
        {
            get
            {
                return strengthToGive;
            }
            set
            {
                strengthToGive = value;
                Stack = Stack;
            }
        }

        public int GivenStrength { get; protected set; }

        private int count;

        private int strengthToGive;

        public FleshHeap() : base() { }

        public FleshHeap(Hero owner) : base(owner) { }

        public override void Enabled()
        {
            var fleshHeap = Hero.Abilities.FirstOrDefault(ability => ability.Name == "Flesh heap");

            if (fleshHeap == default)
            {
                return;
            }

            IsVisible = false;

            base.Enabled();
        }

        public override void Executed()
        {
            Stack++;

            base.Executed();
        }
    }
}
