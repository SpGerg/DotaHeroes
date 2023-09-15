using CustomPlayerEffects;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Interfaces;
using Exiled.API.Features;
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

        public override string Description => "Prevents the owner from moving, attacking and using abilities";

        public override DispelType DispelType { get; set; } = DispelType.Strong;

        public int Duration => 3;

        public override EffectClassType EffectType => EffectClassType.Negative;

        public Stun(Player owner) : base(owner)
        {
        }

        public override bool Enable()
        {
            Owner.EnableEffect<Ensnared>();

            Timing.CallDelayed(Duration, () =>
            {
                Owner.DisableEffect<Ensnared>();
            });

            return true;
        }

        public override bool Execute()
        {
            return Enable();
        }
    }
}
