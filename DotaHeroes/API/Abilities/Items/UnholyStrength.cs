using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Interfaces;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Abilities.Items
{
    public class UnholyStrength : ToggleAbility
    {
        public override string Name => "Unholy strength";

        public override string Slug => "unholy_strength";

        public override string Description => "Unholy strength";

        public override string Lore => "Unholy strength";

        public override AbilityType AbilityType => AbilityType.Toggle;

        public override TargetType TargetType => TargetType.None;

        private static readonly IReadOnlyDictionary<StatisticsType, Value> Values = new Dictionary<StatisticsType, Value>()
        {
            { StatisticsType.ExtraAttackDamage, new Value(35, false) },
            { StatisticsType.Strength, new Value(25, false) },
            { StatisticsType.Armor, new Value(4, false) }
        };

        public UnholyStrength() : base() { }

        public override bool Activate(Hero hero, ArraySegment<string> arguments, out string response)
        {
            hero.HeroStatistics.AddOrReduceStatistics(Values, false);

            Timing.RunCoroutine(DamageCoroutine(hero));

            response = "Armlet is enabled";
            return true;
        }

        public override bool Deactivate(Hero hero, ArraySegment<string> arguments, out string response)
        {
            hero.HeroStatistics.AddOrReduceStatistics(Values, true);

            Timing.RunCoroutine(DamageCoroutine(hero));

            response = "Armlet is disabled";
            return true;
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
