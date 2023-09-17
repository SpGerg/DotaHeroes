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
    public class FleshHeap : Effect
    {
        public override string Name => "Flesh heap";

        public override string Description { get; protected set; } = "Flesh heap";

        public override EffectClassType EffectClassType => EffectClassType.Positive;

        public int Count { get; protected set; }

        public FleshHeap() : base() { }

        public FleshHeap(Player owner) : base(owner) { }

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
