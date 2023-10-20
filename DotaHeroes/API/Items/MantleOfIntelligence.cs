using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System.Collections.Generic;

namespace DotaHeroes.API.Items
{
    public class MantleOfIntelligence : Item
    {
        public override string Name => "Mantle Of Intelligence";

        public override string Slug => "mantle_of_intelligence";

        public override string Description => "mantle_of_intelligence";

        public override string Lore => "mantle_of_intelligence is mantle_of_intelligence";

        public override Ability MainAbility { get; } = API.GetAbilityOrDefaultBySlug("mantle_of_intelligence");

        public override List<Ability> Passives { get; } = Ability.ToAbilitiesFromStringList(Plugin.Instance.Config.Items["mantle_of_intelligence"].Passives);

        public override IReadOnlyList<Item> Ingredients => GetItemsFromStringList(Plugin.Instance.Config.Items["mantle_of_intelligence"].Ingredients);

        public override IReadOnlyList<Item> ItemsFromThisItem => GetItemsFromStringList(Plugin.Instance.Config.Items["mantle_of_intelligence"].ItemsFromThisItems);

        public override IReadOnlyDictionary<StatisticsType, Value> Statistics { get; } = Plugin.Instance.Config.Items["mantle_of_intelligence"].Statistics;

        public override int Cost { get; } = Plugin.Instance.Config.Items["mantle_of_intelligence"].Cost;

        public override int SellCost { get; } = Plugin.Instance.Config.Items["mantle_of_intelligence"].SellCost;

        public MantleOfIntelligence() { }

        protected MantleOfIntelligence(Hero owner) : base(owner) { }

        public override Item Create(Hero owner)
        {
            return new MantleOfIntelligence(owner);
        }
    }
}
