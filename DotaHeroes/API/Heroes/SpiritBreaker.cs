using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Heroes
{
    public class SpiritBreaker : Hero
    {
        public override string HeroName => "Spirit breaker";

        public override HeroClassType HeroClassType { get; set; } = HeroClassType.Melee;

        public SpiritBreaker() : base()
        {

        }

        public SpiritBreaker(Player player, SideType sideType) : base(player, sideType)
        {

        }

        public override Hero Create()
        {
            return new SpiritBreaker();
        }

        public override Hero Create(Player player, SideType sideType)
        {
            return new SpiritBreaker(player, sideType);
        }
    }
}
