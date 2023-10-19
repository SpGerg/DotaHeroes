using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Effects
{
    public class Disarm : Effect
    {
        public override string Name => "Disarm";

        public override string Description { get; protected set; } = "Disarm";

        public override EffectClassType EffectClassType => EffectClassType.Negative;

        public override DispelType DispelType { get; set; } = DispelType.Strong;

        public float Duration { get; set; }

        public Disarm() : base() { }

        public Disarm(Hero owner) : base(owner) { }
    }
}
