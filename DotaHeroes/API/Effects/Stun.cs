using CustomPlayerEffects;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Interfaces;

namespace DotaHeroes.API.Effects
{
    public class Stun : Effect, IEffectDuration
    {
        public override string Name => "Stun";

        public override string Slug => "stun";

        public override string Description { get; protected set; } = "Prevents the owner from moving, attacking and using abilities";

        public override DispelType DispelType { get; set; } = DispelType.Strong;

        public override EffectClassType EffectClassType => EffectClassType.Negative;

        public float Duration { get; set; } = 3;

        public bool IsIgnoreSpellImmunity { get; set; }

        public Stun() : base() { }   

        public Stun(Hero owner) : base(owner)
        {
        }

        public override void Enabled()
        {
            if (Owner.TryGetEffect(out SpellImmunity result) && !IsIgnoreSpellImmunity)
            {
                Owner.DisableEffect(this);
                return;
            }

            foreach (var ability in Owner.Abilities)
            {
                ability.Stop(Owner);
            }

            Owner.Player.EnableEffect<Ensnared>();

            base.Enabled();
        }

        public override void Executed()
        {
            Enabled();
        }

        public override void Disabled()
        {
            Owner.Player.DisableEffect<Ensnared>();

            base.Disabled();
        }
    }
}
