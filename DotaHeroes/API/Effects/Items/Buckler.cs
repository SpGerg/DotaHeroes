using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;

namespace DotaHeroes.API.Effects.Items
{
    public class Buckler : Effect
    {
        public override string Name => "Buckler";

        public override string Slug => "buckler";

        public override string Description { get; protected set; } = "Buckler";

        public override EffectClassType EffectClassType => EffectClassType.Positive;

        public double Armor { get; set; }

        public Buckler() : base() { }

        public Buckler(Hero owner) : base(owner) { }

        public override void Enabled()
        {
            Owner.HeroStatistics.AddOrReduceStatistic(StatisticsType.Armor, new Value(Armor, false), false);

            base.Enabled();
        }

        public override void Disabled()
        {
            Owner.HeroStatistics.AddOrReduceStatistic(StatisticsType.Armor, new Value(Armor, false), true);

            base.Disabled();
        }
    }
}
