﻿using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Effects
{
    public class Break : Effect, IEffectDuration
    {
        public override string Name => "Break";

        public override string Description { get; protected set; } = "Disable passives abilties";

        public override EffectClassType EffectClassType => EffectClassType.Negative;

        public float Duration { get; set; }

        public Break() : base() { }

        public Break(Hero owner) : base(owner) { }
    }
}
