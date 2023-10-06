using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Interfaces;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Effect = DotaHeroes.API.Features.Effect;

namespace DotaHeroes.API.Effects.Pudge
{
    public class FleshHeapShield : Effect, IDamageBlock, IEffectDuration
    {
        public override string Name => "Flesh heap shield";

        public override string Description { get; protected set; } = "Your blocking damage";

        public override EffectClassType EffectClassType => EffectClassType.Positive;

        public override DispelType DispelType { get; set; } = DispelType.None;

        public int DamageBlock { get; set; }

        public IReadOnlyList<DamageType> DamageTypesToBlock => new List<DamageType>() { DamageType.None };

        public float Duration { get; set; } = 7;

        public FleshHeapShield() : base() { }

        public FleshHeapShield(Hero owner) : base(owner) { }
    }
}
