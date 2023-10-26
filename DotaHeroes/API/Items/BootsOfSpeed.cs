using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Items
{
    public class BootsOfSpeed : Item
    {
        public override string Name => "Boots of speed";

        public override string Slug => "boots_of_speed";

        public override string Description => "Boots of speed";

        public override string Lore => "Boots of speed";

        public override Ability MainAbility { get; } = DTAPI.GetAbilityOrDefaultBySlug(Plugin.Instance.Config.Items["boots_of_speed"].Ability);

        public override IReadOnlyList<Item> Ingredients { get; } = GetItemsFromStringList(Plugin.Instance.Config.Items["boots_of_speed"].Ingredients);

        public override IReadOnlyList<Item> ItemsFromThisItem { get; } = GetItemsFromStringList(Plugin.Instance.Config.Items["boots_of_speed"].ItemsFromThisItems);

        public override IReadOnlyDictionary<StatisticsType, Value> Statistics { get; } = Plugin.Instance.Config.Items["boots_of_speed"].Statistics;

        public override int Cost { get; } = Plugin.Instance.Config.Items["boots_of_speed"].Cost;

        public override int SellCost { get; } = Plugin.Instance.Config.Items["boots_of_speed"].SellCost;

        public BootsOfSpeed() { }

        protected BootsOfSpeed(Hero owner) : base(owner) { }

        public override Item Create(Hero owner)
        {
            return new BootsOfSpeed(owner);
        }

        public override string ToString()
        {
            return $"{Name}: {(MainAbility as ToggleAbility).IsActive}";
        }
    }
}
