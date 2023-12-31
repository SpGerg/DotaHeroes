﻿using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Statistics;
using Exiled.API.Features;
using PlayerRoles;
using System.Collections.Generic;

namespace DotaHeroes.API.Heroes
{
    public class Pudge : Hero
    {
        public override string HeroName => "Pudge";

        public override string Slug => "pudge";

        public override List<RoleTypeId> ChangeRoles { get; set; } = Plugin.Instance.Config.Heroes["pudge"].ChangeRoles;

        public override HeroClassType HeroClassType { get; set; } = Plugin.Instance.Config.Heroes["pudge"].HeroClassType;

        public Pudge() : base()
        {
            SideType = SideType.Dire;

            Abilities = Ability.ToAbilitiesFromStringList(this, Plugin.Instance.Config.Heroes[Slug].Abilties, true);

            HeroStatistics = new HeroStatistics(Plugin.Instance.Config.Heroes[Slug].DefaultHeroStatistics.ToHeroStatistics(this), this);
        }

        protected Pudge(Player player, SideType sideType) : base(player, sideType)
        {
            Abilities = Ability.ToAbilitiesFromStringList(this, Plugin.Instance.Config.Heroes[Slug].Abilties, true);

            HeroStatistics = new HeroStatistics(Plugin.Instance.Config.Heroes[Slug].DefaultHeroStatistics.ToHeroStatistics(this), this);
        }

        public override Hero Create(Player player, SideType sideType)
        {
            return new Pudge(player, sideType);
        }
    }
}
