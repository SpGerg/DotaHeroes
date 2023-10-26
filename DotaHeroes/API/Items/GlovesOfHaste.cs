using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System.Collections.Generic;

namespace DotaHeroes.API.Items
{
    public class GlovesOfHaste : Item
    {
        public override string Name => "Gloves of haste";

        public override string Slug => "gloves_of_haste";

        public override string Description => "Gloves of haste";

        public override string Lore => "Gloves of haste";

        public override Ability MainAbility { get; } = DTAPI.GetAbilityOrDefaultBySlug(Plugin.Instance.Config.Items["gloves_of_haste"].Ability);

        public override List<Ability> Passives { get; } = Ability.ToAbilitiesFromStringList(Plugin.Instance.Config.Items["gloves_of_haste"].Passives);

        public override IReadOnlyList<Item> Ingredients { get; } = GetItemsFromStringList(Plugin.Instance.Config.Items["gloves_of_haste"].Ingredients);

        public override IReadOnlyList<Item> ItemsFromThisItem { get; } = GetItemsFromStringList(Plugin.Instance.Config.Items["gloves_of_haste"].ItemsFromThisItems);

        public override IReadOnlyDictionary<StatisticsType, Value> Statistics { get; } = Plugin.Instance.Config.Items["gloves_of_haste"].Statistics;

        public override int Cost { get; } = Plugin.Instance.Config.Items["gloves_of_haste"].Cost;

        public override int SellCost { get; } = Plugin.Instance.Config.Items["gloves_of_haste"].SellCost;

        public GlovesOfHaste() : base()
        {
        }

        protected GlovesOfHaste(Hero owner) : base(owner)
        {
        }

        public override Item Create(Hero owner)
        {
            return new GlovesOfHaste(owner);
        }
    }
}
