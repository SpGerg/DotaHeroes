using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System.Collections.Generic;

namespace DotaHeroes.API.Items
{
    public class Bracer : Item
    {
        public override string Name => "Bracer";

        public override string Slug => "bracer";

        public override string Description => "Bracer";

        public override string Lore => "Bracer";

        public override Ability MainAbility { get; } = DTAPI.GetAbilityOrDefaultBySlug(Plugin.Instance.Config.Items["bracer"].Ability);

        public override List<Ability> Passives { get; } = Ability.ToAbilitiesFromStringList(Plugin.Instance.Config.Items["bracer"].Passives);

        public override IReadOnlyList<Item> Ingredients => GetItemsFromStringList(Plugin.Instance.Config.Items["bracer"].Ingredients);

        public override IReadOnlyList<Item> ItemsFromThisItem => GetItemsFromStringList(Plugin.Instance.Config.Items["bracer"].ItemsFromThisItems);

        public override IReadOnlyDictionary<StatisticsType, Value> Statistics { get; } = Plugin.Instance.Config.Items["bracer"].Statistics;

        public override int Cost { get; } = Plugin.Instance.Config.Items["bracer"].Cost;

        public override int SellCost { get; } = Plugin.Instance.Config.Items["bracer"].SellCost;

        public Bracer() : base()
        {
        }

        protected Bracer(Hero owner) : base(owner)
        {
        }

        public override Item Create(Hero owner)
        {
            return new Bracer(owner);
        }
    }
}
