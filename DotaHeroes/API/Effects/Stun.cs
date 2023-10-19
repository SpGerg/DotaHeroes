using CustomPlayerEffects;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Interfaces;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Effects
{
    public class Stun : Effect, IEffectDuration
    {
        public override string Name => "Stun";

        public override string Description { get; protected set; } = "Prevents the owner from moving, attacking and using abilities";

        public override DispelType DispelType { get; set; } = DispelType.Strong;

        public float Duration { get; set; } = 5;

        public override EffectClassType EffectClassType => EffectClassType.Negative;

        public bool IsIgnoreSpellImmunity { get; set; }

        public Stun() : base() { }   

        public Stun(Hero owner) : base(owner)
        {
        }

        public override void Enable()
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

            base.Enable();
        }

        public override void Execute()
        {
            Enable();
        }

        public override void Disable()
        {
            Owner.Player.DisableEffect<Ensnared>();

            base.Disable();
        }
    }
}
