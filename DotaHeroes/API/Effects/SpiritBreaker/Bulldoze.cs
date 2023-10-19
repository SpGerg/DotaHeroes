using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Effects.SpiritBreaker
{
    public class Bulldoze : Effect, IResistanceModifier, IEffectDuration
    {
        public override string Name => "Bulldoze";

        public override string Description { get; protected set; } = "Bulldoze";

        public override EffectClassType EffectClassType => EffectClassType.Positive;

        public override DispelType DispelType { get; set; } = DispelType.Basic;

        public decimal MagicResistance { get; set; } = 0;

        public decimal EffectResistance { get; set; } = 35;

        public float Duration { get; set; } = 3;

        public Bulldoze() : base() { }

        public Bulldoze(Hero owner) : base(owner) { }
    }
}
