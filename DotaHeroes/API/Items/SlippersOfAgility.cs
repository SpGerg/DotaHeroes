using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Items
{
    public class SlippersOfAgility : Item
    {
        public override string Name => "Slippers Of Agility";

        public override string Slug => "slippers_of_agility";

        public override string Description => "slippers_of_agility";

        public override string Lore => "slippers_of_agility is slippers_of_agility";

        public override Ability MainAbility { get; } = API.GetAbilityOrDefaultBySlug("slippers_of_agility");

        public override List<Ability> Passives { get; } = Ability.ToAbilitiesFromStringList(Plugin.Instance.Config.Items["slippers_of_agility"].Passives);

        public override IReadOnlyList<Item> Ingredients => GetItemsFromStringList(Plugin.Instance.Config.Items["slippers_of_agility"].Ingredients);

        public override IReadOnlyList<Item> ItemsFromThisItem => GetItemsFromStringList(Plugin.Instance.Config.Items["slippers_of_agility"].ItemsFromThisItems);

        public override IReadOnlyDictionary<StatisticsType, Value> Statistics { get; } = Plugin.Instance.Config.Items["slippers_of_agility"].Statistics;

        public override int Cost { get; } = Plugin.Instance.Config.Items["slippers_of_agility"].Cost;

        public override int SellCost { get; } = Plugin.Instance.Config.Items["slippers_of_agility"].SellCost;

        public SlippersOfAgility() { }

        protected SlippersOfAgility(Hero owner) : base(owner) { }

        public override Item Create(Hero owner)
        {
            return new SlippersOfAgility(owner);
        }
    }
}
