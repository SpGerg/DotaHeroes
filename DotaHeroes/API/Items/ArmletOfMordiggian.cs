using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System.Collections.Generic;

namespace DotaHeroes.API.Items
{
    public class ArmletOfMordiggian : Item
    {
        public override string Name => "Armlet of Mordiggian";

        public override string Slug => "armlet_of_mordiggian";

        public override string Description => "Armlet of Mordiggian";

        public override string Lore => "Armlet of Mordiggian";

        public override Ability MainAbility { get; } = DTAPI.GetAbilityOrDefaultBySlug(Plugin.Instance.Config.Items["armlet_of_mordiggian"].Ability);

        public override IReadOnlyDictionary<StatisticsType, Value> Statistics { get; } = Plugin.Instance.Config.Items["armlet_of_mordiggian"].Statistics;

        public override IReadOnlyList<Item> Ingredients { get; } = GetItemsFromStringList(Plugin.Instance.Config.Items["armlet_of_mordiggian"].Ingredients);

        public override IReadOnlyList<Item> ItemsFromThisItem { get; } = GetItemsFromStringList(Plugin.Instance.Config.Items["armlet_of_mordiggian"].ItemsFromThisItems);

        public override int Cost { get; } = Plugin.Instance.Config.Items["armlet_of_mordiggian"].Cost;

        public override int SellCost { get; } = Plugin.Instance.Config.Items["armlet_of_mordiggian"].SellCost;

        public ArmletOfMordiggian() { }

        protected ArmletOfMordiggian(Hero owner) : base(owner) { }

        public override Item Create(Hero owner)
        {
            return new ArmletOfMordiggian(owner);
        }

        public override string ToString()
        {
            return $"{Name}: {(MainAbility as ToggleAbility).IsActive}";
        }
    }
}
