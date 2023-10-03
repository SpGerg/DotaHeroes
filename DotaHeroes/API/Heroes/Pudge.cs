using DotaHeroes.API.Abilities;
using DotaHeroes.API.Abilities.Pudge;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Statistics;
using Exiled.API.Features;
using Exiled.API.Features.Toys;
using System.Collections.Generic;

namespace DotaHeroes.API.Heroes
{
    public class Pudge : Hero
    {
        public override string HeroName => "Pudge";

        public override List<Ability> Abilities { get; } = Ability.ToAbilitiesFromStringList(Plugin.Instance.Config.Heroes["Pudge"].Abilties);

        public override HeroClassType HeroClassType { get; set; } = HeroClassType.Melee;

        public Pudge() : base()
        {
            SideType = SideType.Dire;

            HeroStatistics = new HeroStatistics(Plugin.Instance.Config.Heroes[HeroName].DefaultHeroStatistics, this);
        }

        public Pudge(Hero hero) : base(hero)
        {
            HeroStatistics = new HeroStatistics(Plugin.Instance.Config.Heroes[HeroName].DefaultHeroStatistics, this);
        }

        public Pudge(Player player, SideType sideType) : base(player, sideType)
        {
            HeroStatistics = new HeroStatistics(Plugin.Instance.Config.Heroes[HeroName].DefaultHeroStatistics, this);
        }

        public override Hero Create()
        {
            return new Pudge();
        }

        public override Hero Create(Hero hero)
        {
            return new Pudge(hero);
        }

        public override Hero Create(Player player, SideType sideType)
        {
            return new Pudge(player, sideType);
        }
    }
}
