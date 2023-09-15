using DotaHeroes.API.Abilities.Pudge;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Statistics;
using Exiled.API.Features;
using System.Collections.Generic;

namespace DotaHeroes.API.Heroes
{
    public class Pudge : Hero
    {
        public override string HeroName => "Pudge";

        public override IReadOnlyList<Ability> Abilities => new List<Ability>
        {
            new MeatHook(Player),
            new Rot(Player)
        };

        public override HeroStatistics HeroStatistics => new HeroStatistics(AttributeType.Strength,
            new HealthAndManaStatistics(120, 75, 120, 75), 
            new AttackStatistics(48, 0, 100),
            new ArmorStatistics(),
            new ResistanceStatistics(25, 25),
            new SpeedStatistics(0));

        public Pudge(Player player) : base(player)
        {
            SideType = SideType.Dire;
        }
    }
}
