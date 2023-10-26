using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Effects.Items
{
    public class PhaseSpeed : Effect, IEffectDuration
    {
        public override string Name => "Phase speed";

        public override string Slug => "phase_speed";

        public override string Description { get; protected set; } = "Extra speed from phase";

        public override DispelType DispelType { get; set; } = DispelType.Dead;

        public override EffectClassType EffectClassType => EffectClassType.Positive;

        public sbyte ExtraSpeed { get; set; }

        public float Duration { get; set; }

        public PhaseSpeed() { }

        public PhaseSpeed(Hero owner) : base(owner) { }

        public override void Enabled()
        {
            Owner.HeroStatistics.Speed.Speed += ExtraSpeed;

            base.Enabled();
        }

        public override void Disabled()
        {
            Owner.HeroStatistics.Speed.Speed -= ExtraSpeed;

            base.Disabled();
        }
    }
}
