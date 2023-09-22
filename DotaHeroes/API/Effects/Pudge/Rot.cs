﻿using CustomPlayerEffects;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Interfaces;
using Exiled.API.Features;
using Exiled.API.Features.Toys;
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

        public override string Description { get; protected set; } = "Rot";

        public override EffectClassType EffectClassType => EffectClassType.Negative;

        public override DispelType DispelType { get; set; } = DispelType.NotDispelling;

        public DamageOverTime DamageOverTime => new DamageOverTime(30, DamageType.Magical, -1, 1f);

        public Rot() : base() { }

        public Rot(Player owner) : base(owner)
        {
            
        }

        public override bool Enable()
        {
            Hero.HeroStatistics.Speed.Speed -= 10;
            DamageOverTime.Run(Hero);
             
            base.Enable();

            return true;
        }

        public override bool Disable()
        {
            Hero.HeroStatistics.Speed.Speed += 10;
            DamageOverTime.IsEnabled = false;

            base.Disable();

            return true;
        }
    }
}
