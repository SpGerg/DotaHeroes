using DotaHeroes.API.Abilities.Items;
using DotaHeroes.API.Effects.Items;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Auras.Items
{
    public class BucklerAura : Aura
    {
        public override string Name => "Buckler aura";

        public override string Slug => "buckler_aura";

        public override string Description => "Buckler aura";

        public override TargetType TargetType => TargetType.ToFriend;

        public override float Radius { get; set; } = 5;

        public decimal Armor { get; set; }

        private BucklerAura() { }

        public BucklerAura(Hero owner) : base(owner)
        {
            
        }

        public override void Added(Hero hero)
        {
            hero.HeroStatistics.AddOrReduceStatistic(StatisticsType.Armor, new Value(Armor, false), false); //Im recomended using effect, this is shit method.

            base.Added(hero);
        }

        public override void Removed(Hero hero)
        {
            hero.HeroStatistics.AddOrReduceStatistic(StatisticsType.Armor, new Value(Armor, false), true);

            base.Removed(hero);
        }

        public override Ability Create()
        {
            return new BucklerAura();
        }
    }
}
