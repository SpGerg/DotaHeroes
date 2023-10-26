using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Interfaces;

namespace DotaHeroes.API.Effects
{
    public class Disarm : Effect, IEffectDuration
    {
        public override string Name => "Disarm";

        public override string Slug => "disarm";

        public override string Description { get; protected set; } = "Disarm";

        public override EffectClassType EffectClassType => EffectClassType.Negative;

        public override DispelType DispelType { get; set; } = DispelType.Strong;

        public float Duration { get; set; }

        public Disarm() : base() { }

        public Disarm(Hero owner) : base(owner) { }
    }
}
