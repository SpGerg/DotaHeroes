﻿using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Effect = DotaHeroes.API.Features.Effect;

namespace DotaHeroes.API.Effects.Pudge
{
    public class FleshHeap : Effect
    {
        public override string Name => "Flesh heap";

        public override string Description { get; protected set; } = "Flesh heap";

        public override EffectClassType EffectClassType => EffectClassType.Positive;

        public int Count {
            get
            {
                return count;
            }
            set
            {
                count = value;

                Hero.HeroStatistics.Strength -= GivenStrength;
                Hero.HeroStatistics.Strength += StrengthToGive * count;
                GivenStrength += StrengthToGive;
            }
        }

        public int StrengthToGive
        {
            get
            {
                return strengthToGive;
            }
            set
            {
                strengthToGive = value;
                Count = Count;
            }
        }

        public int GivenStrength { get; protected set; }

        private int count;

        private int strengthToGive;

        public FleshHeap() : base() { }

        public FleshHeap(Hero owner) : base(owner) { }

        public override bool Enable()
        {
            var fleshHeap = Hero.Abilities.FirstOrDefault(ability => ability.Name == "Flesh heap");

            if (fleshHeap == default)
            {
                return false;
            }

            IsVisible = false;

            return base.Enable();
        }

        public override bool Execute()
        {
            Count++;

            return base.Execute();
        }
    }
}
