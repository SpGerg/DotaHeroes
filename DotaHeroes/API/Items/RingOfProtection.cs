using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Items
{
    public class RingOfProtection : Item
    {
        public override string Name => "Ring of protection";

        public override string Slug => "ring_of_protection";

        public override string Description => "Ring of protection";

        public override string Lore => "Ring of protection";

        public override Ability MainAbility { get; } = DTAPI.GetAbilityOrDefaultBySlug(Plugin.Instance.Config.Items["ring_of_protection"].Ability);

        public override List<Ability> Passives { get; } = Ability.ToAbilitiesFromStringList(Plugin.Instance.Config.Items["ring_of_protection"].Passives);

        public override IReadOnlyList<Item> Ingredients { get; } = GetItemsFromStringList(Plugin.Instance.Config.Items["ring_of_protection"].Ingredients);

        public override IReadOnlyList<Item> ItemsFromThisItem { get; } = GetItemsFromStringList(Plugin.Instance.Config.Items["ring_of_protection"].ItemsFromThisItems);

        public override IReadOnlyDictionary<StatisticsType, Value> Statistics { get; } = Plugin.Instance.Config.Items["ring_of_protection"].Statistics;

        public override int Cost { get; } = Plugin.Instance.Config.Items["ring_of_protection"].Cost;

        public override int SellCost { get; } = Plugin.Instance.Config.Items["ring_of_protection"].SellCost;

        public RingOfProtection() { }

        protected RingOfProtection(Hero owner) : base(owner) { }

        public override Item Create(Hero owner)
        {
            return new RingOfProtection(owner);
        }
    }
}
