using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Items
{
    public class VitalityBooster : Item
    {
        public override string Name => "Vitality booster";

        public override string Slug => "vitality_booster";

        public override string Description => "Vitality booster";

        public override string Lore => "Vitality booster";

        public override Ability MainAbility { get; } = DTAPI.GetAbilityOrDefaultBySlug(Plugin.Instance.Config.Items["vitality_booster"].Ability);

        public override List<Ability> Passives { get; } = Ability.ToAbilitiesFromStringList(Plugin.Instance.Config.Items["vitality_booster"].Passives);

        public override IReadOnlyList<Item> Ingredients { get; } = GetItemsFromStringList(Plugin.Instance.Config.Items["vitality_booster"].Ingredients);

        public override IReadOnlyList<Item> ItemsFromThisItem { get; } = GetItemsFromStringList(Plugin.Instance.Config.Items["vitality_booster"].ItemsFromThisItems);

        public override IReadOnlyDictionary<StatisticsType, Value> Statistics { get; } = Plugin.Instance.Config.Items["vitality_booster"].Statistics;

        public override int Cost { get; } = Plugin.Instance.Config.Items["vitality_booster"].Cost;

        public override int SellCost { get; } = Plugin.Instance.Config.Items["vitality_booster"].SellCost;

        public VitalityBooster() { }

        protected VitalityBooster(Hero owner) : base(owner) { }

        public override Item Create(Hero owner)
        {
            return new VitalityBooster(owner);
        }
    }
}
