using CustomPlayerEffects;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Effects.Pudge
{
    public class Rot : Features.Effect
    {
        public override string Name => "Rot";

        public override string Description => "Rot";

        public override EffectClassType EffectClassType => EffectClassType.Negative;

        public Rot() : base() { }

        public Rot(Player owner) : base(owner)
        {
        }

        public override bool Enable()
        {
            Owner.EnableEffect<Disabled>();

            return true;
        }

        public override bool Disable()
        {
            Owner.DisableEffect<Disabled>();

            return true;
        }
    }
}
