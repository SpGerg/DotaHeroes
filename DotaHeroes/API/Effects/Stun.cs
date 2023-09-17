using CustomPlayerEffects;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
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
    public class Stun : Features.Effect, IEffectDuration
    {
        public override string Name => "Stun";

        public override string Description { get; protected set; } = "Prevents the owner from moving, attacking and using abilities";

        public override DispelType DispelType { get; set; } = DispelType.Strong;

        public int Duration => 3;

        public override EffectClassType EffectClassType => EffectClassType.Negative;

        public Stun() : base() { }   

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
