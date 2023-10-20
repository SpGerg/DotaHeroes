using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System.Collections.Generic;

namespace DotaHeroes.API.Items
{
    public class NullTalisman : Item
    {
        public override string Name => "Null talisman";

        public override string Slug => "null_talisman";

        public override string Description => "Nulli";

        public override string Lore => "talisman is talisman";

        public override Ability MainAbility { get; } = API.GetAbilityOrDefaultBySlug("null_talisman");

        public override List<Ability> Passives { get; } = Ability.ToAbilitiesFromStringList(Plugin.Instance.Config.Items["null_talisman"].Passives);

        public override IReadOnlyList<Item> Ingredients => GetItemsFromStringList(Plugin.Instance.Config.Items["null_talisman"].Ingredients);

        public override IReadOnlyList<Item> ItemsFromThisItem => GetItemsFromStringList(Plugin.Instance.Config.Items["null_talisman"].ItemsFromThisItems);

        public override IReadOnlyDictionary<StatisticsType, Value> Statistics { get; } = Plugin.Instance.Config.Items["null_talisman"].Statistics;

        public override int Cost { get; } = Plugin.Instance.Config.Items["null_talisman"].Cost;

        public override int SellCost { get; } = Plugin.Instance.Config.Items["null_talisman"].SellCost;

        public NullTalisman() { }

        protected NullTalisman(Hero owner) : base(owner) { }

        public override Item Create(Hero owner)
        {
            return new NullTalisman(owner);
        }
    }
}
