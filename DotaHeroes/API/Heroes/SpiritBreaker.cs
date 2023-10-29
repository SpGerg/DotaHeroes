using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Statistics;
using Exiled.API.Features;
using PlayerRoles;
using System.Collections.Generic;

namespace DotaHeroes.API.Heroes
{
    public class SpiritBreaker : Hero
    {
        public override string HeroName => "Spirit breaker";

        public override string Slug => "spirit_breaker";

        public override List<RoleTypeId> ChangeRoles { get; set; } = Plugin.Instance.Config.Heroes["spirit_breaker"].ChangeRoles;

        public override HeroClassType HeroClassType { get; set; } = HeroClassType.Melee;

        public SpiritBreaker() : base()
        {
            SideType = SideType.Dire;

            Abilities = Ability.ToAbilitiesFromStringList(this, Plugin.Instance.Config.Heroes[Slug].Abilties);

            HeroStatistics = new HeroStatistics(Plugin.Instance.Config.Heroes[Slug].DefaultHeroStatistics.ToHeroStatistics(this), this);
        }

        protected SpiritBreaker(Player player, SideType sideType) : base(player, sideType)
        {
            SideType = sideType;

            Abilities = Ability.ToAbilitiesFromStringList(this, Plugin.Instance.Config.Heroes[Slug].Abilties);

            HeroStatistics = new HeroStatistics(Plugin.Instance.Config.Heroes[Slug].DefaultHeroStatistics.ToHeroStatistics(this), this);
        }

        public override Hero Create(Player player, SideType sideType)
        {
            return new SpiritBreaker(player, sideType);
        }
    }
}
