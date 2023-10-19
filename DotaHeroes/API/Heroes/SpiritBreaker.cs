using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Statistics;
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

        public override string Slug => "spirit_breaker";

        public override List<Ability> Abilities { get; } = Ability.ToAbilitiesFromStringList(Plugin.Instance.Config.Heroes["spirit_breaker"].Abilties);

        public override HeroClassType HeroClassType { get; set; } = HeroClassType.Melee;

        public SpiritBreaker() : base()
        {
            SideType = SideType.Dire;

            HeroStatistics = new HeroStatistics(Plugin.Instance.Config.Heroes[Slug].DefaultHeroStatistics.ToHeroStatistics(this), this);
        }

        protected SpiritBreaker(Player player, SideType sideType) : base(player, sideType)
        {
            SideType = sideType;

            HeroStatistics = new HeroStatistics(Plugin.Instance.Config.Heroes[Slug].DefaultHeroStatistics.ToHeroStatistics(this), this);
        }

        public override Hero Create(Player player, SideType sideType)
        {
            return new SpiritBreaker(player, sideType);
        }
    }
}
