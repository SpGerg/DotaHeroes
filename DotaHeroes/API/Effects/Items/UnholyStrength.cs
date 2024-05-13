using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using MEC;
using System.Collections.Generic;

namespace DotaHeroes.API.Effects.Items
{
    public class UnholyStrength : Effect
    {
        public override string Name => "Unholy strength";

        public override string Slug => "unholy_strength";

        public override string Description { get; protected set; } = "Unholy strength";

        public override DispelType DispelType { get; set; } = DispelType.Dead;

        public override EffectClassType EffectClassType => EffectClassType.Negative;

        public double ExtraAttackDamage { get; set; }

        public double Strength { get; set; }

        public double Armor { get; set; }

        private IReadOnlyDictionary<StatisticsType, Value> Stats => new Dictionary<StatisticsType, Value>()
        {
            { StatisticsType.ExtraAttackDamage, new Value(ExtraAttackDamage, false) },
            { StatisticsType.Strength, new Value(Strength, false) },
            { StatisticsType.Armor, new Value(Armor, false) }
        };

        public UnholyStrength() : base() { }

        public UnholyStrength(Hero owner) : base(owner) { }

        public override void Enabled()
        {
            Owner.HeroStatistics.AddOrReduceStatistics(Stats, false);

            Timing.RunCoroutine(DamageCoroutine(Owner));

            base.Enabled();
        }

        public override void Disabled()
        {
            Owner.HeroStatistics.AddOrReduceStatistics(Stats, true);

            base.Disabled();
        }

        private IEnumerator<float> DamageCoroutine(Hero hero)
        {
            yield return Timing.WaitForSeconds(1);

            while (IsActive)
            {
                hero.TakeDamage(hero, 45, DamageType.Pure, false);

                yield return Timing.WaitForSeconds(1);
            }
        }
    }
}
