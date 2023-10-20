using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Interfaces;

namespace DotaHeroes.API.Effects
{
    public class Silence : Effect, IEffectDuration
    {
        public override string Name => "Silence";

        public override string Description { get; protected set; } = "Silence";

        public override EffectClassType EffectClassType => EffectClassType.Negative;

        public override DispelType DispelType { get; set; } = DispelType.Basic;

        public float Duration { get; set; }

        public Silence() : base() { }

        public Silence(Hero owner) : base(owner) { }
    }
}
