using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Items
{
    public class RingOfHealth : Item
    {
        public override string Name => "Ring of health";

        public override string Slug => "ring_of_health";

        public override string Description => "Ring of health";

        public override string Lore => "Ring of health";

        public override Ability MainAbility { get; } = DTAPI.GetAbilityOrDefaultBySlug(Plugin.Instance.Config.Items["ring_of_health"].Ability);

        public override List<Ability> Passives { get; } = Ability.ToAbilitiesFromStringList(Plugin.Instance.Config.Items["ring_of_health"].Passives);

        public override IReadOnlyList<Item> Ingredients { get; } = GetItemsFromStringList(Plugin.Instance.Config.Items["ring_of_health"].Ingredients);

        public override IReadOnlyList<Item> ItemsFromThisItem { get; } = GetItemsFromStringList(Plugin.Instance.Config.Items["ring_of_health"].ItemsFromThisItems);

        public override IReadOnlyDictionary<StatisticsType, Value> Statistics { get; } = Plugin.Instance.Config.Items["ring_of_health"].Statistics;

        public override int Cost { get; } = Plugin.Instance.Config.Items["ring_of_health"].Cost;

        public override int SellCost { get; } = Plugin.Instance.Config.Items["ring_of_health"].SellCost;

        public RingOfHealth() { }

        protected RingOfHealth(Hero owner) : base(owner) { }

        public override Item Create(Hero owner)
        {
            return new RingOfHealth(owner);
        }
    }
}
