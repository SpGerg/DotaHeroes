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

        public override List<Ability> Abilities => new List<Ability>
        {
            new MeatHook(),
            new Rot(),
            new FleshHeap(),
            new Dismember()
        };

        public override HeroClassType HeroClassType { get; set; } = HeroClassType.Melee;

        public Pudge() : base()
        {
            SideType = SideType.Dire;

            HeroStatistics = new HeroStatistics(AttributeType.Strength,
            3.0f,
            1.5f,
            0.5f,
            new HealthAndManaStatistics(120, 75, 120, 75),
            new AttackStatistics(48, 0, 100, 0.6f),
            new ArmorStatistics(),
            new ResistanceStatistics(25, 25),
            new SpeedStatistics(this, 0));
        }

        public Pudge(Player player, SideType sideType) : base(player, sideType)
        {
            HeroStatistics = new HeroStatistics(AttributeType.Strength,
            3.0f,
            1.5f,
            0.5f,
            new HealthAndManaStatistics(120, 75, 120, 75),
            new AttackStatistics(48, 0, 100, 0.6f),
            new ArmorStatistics(),
            new ResistanceStatistics(),
            new SpeedStatistics(this, 0));
        }

        public override Hero Create()
        {
            return new Pudge();
        }

        public override Hero Create(Player player, SideType sideType)
        {
            return new Pudge(player, sideType);
        }
    }
}
