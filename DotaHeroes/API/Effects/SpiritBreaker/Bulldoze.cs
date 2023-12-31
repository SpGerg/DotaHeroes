﻿using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Interfaces;

namespace DotaHeroes.API.Effects.SpiritBreaker
{
    public class Bulldoze : Effect, IResistanceModifier, IEffectDuration
    {
        public override string Name => "Bulldoze";

        public override string Slug => "bulldoze";

        public override string Description { get; protected set; } = "Bulldoze";

        public override EffectClassType EffectClassType => EffectClassType.Positive;

        public override DispelType DispelType { get; set; } = DispelType.Basic;

        public double MagicResistance { get; set; } = 0;

        public double EffectResistance { get; set; } = 35;

        public float Duration { get; set; } = 3;

        public Bulldoze() : base() { }

        public Bulldoze(Hero owner) : base(owner) { }
    }
}
