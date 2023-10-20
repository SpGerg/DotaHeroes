using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Interfaces;

namespace DotaHeroes.API.Effects
{
    public class SpellImmunity : Effect, IEffectDuration
    {
        public override string Name => "Spell immunity";

        public override string Description { get; protected set; } = "Spell immunity";

        public override EffectClassType EffectClassType => EffectClassType.Positive;

        public override DispelType DispelType { get; set; } = DispelType.Dead;

        public float Duration { get; set; } = 3;

        public SpellImmunity() : base() { }

        public SpellImmunity(Hero owner) : base(owner) { }
    }
}
