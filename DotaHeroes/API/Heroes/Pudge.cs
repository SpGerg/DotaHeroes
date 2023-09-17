using DotaHeroes.API.Abilities;
using DotaHeroes.API.Abilities.Pudge;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Statistics;
using Exiled.API.Features;
using System.Collections.Generic;

namespace DotaHeroes.API.Heroes
{
    public class Pudge : Hero
    {
        public override string HeroName => "Pudge";

        public override List<Ability> Abilities => new List<Ability>
        {
            new MeatHook(),
            new Rot(),
            new FleshHeap(),
            new Dismember()
        };

        public override HeroStatistics HeroStatistics => new HeroStatistics(AttributeType.Strength,
            new HealthAndManaStatistics(120, 75, 120, 75), 
            new AttackStatistics(48, 0, 100, 0.6f),
            new ArmorStatistics(),
            new ResistanceStatistics(25, 25),
            new SpeedStatistics(0));

        public override HeroClassType HeroClassType { get; set; } = HeroClassType.Melee;

        public Pudge(Player player) : base(player)
        {
            SideType = SideType.Dire;
        }
    }
}
