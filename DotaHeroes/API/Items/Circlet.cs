using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System.Collections.Generic;

namespace DotaHeroes.API.Items
{
    public class Circlet : Item
    {
        public override string Name => "Circlet";

        public override string Slug => "circlet";

        public override string Description => "circlet";

        public override string Lore => "circlet is circlet";

        public override Ability MainAbility { get; } = API.GetAbilityOrDefaultBySlug("circlet");

        public override List<Ability> Passives { get; } = Ability.ToAbilitiesFromStringList(Plugin.Instance.Config.Items["circlet"].Passives);

        public override IReadOnlyList<Item> Ingredients => GetItemsFromStringList(Plugin.Instance.Config.Items["circlet"].Ingredients);

        public override IReadOnlyList<Item> ItemsFromThisItem => GetItemsFromStringList(Plugin.Instance.Config.Items["circlet"].ItemsFromThisItems);

        public override IReadOnlyDictionary<StatisticsType, Value> Statistics { get; } = Plugin.Instance.Config.Items["circlet"].Statistics;

        public override int Cost { get; } = Plugin.Instance.Config.Items["circlet"].Cost;

        public override int SellCost { get; } = Plugin.Instance.Config.Items["circlet"].SellCost;

        public Circlet() { }

        protected Circlet(Hero owner) : base(owner) { }

        public override Item Create(Hero owner)
        {
            return new Circlet(owner);
        }
    }
}
