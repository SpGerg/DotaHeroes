using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Items
{
    public class Chainmail : Item
    {
        public override string Name => "Chainmail";

        public override string Slug => "chainmail";

        public override string Description => "Chainmail";

        public override string Lore => "Chainmail";

        public override Ability MainAbility { get; } = DTAPI.GetAbilityOrDefaultBySlug(Plugin.Instance.Config.Items["chainmail"].Ability);

        public override List<Ability> Passives { get; } = Ability.ToAbilitiesFromStringList(Plugin.Instance.Config.Items["chainmail"].Passives);

        public override IReadOnlyList<Item> ItemsFromThisItem { get; } = GetItemsFromStringList(Plugin.Instance.Config.Items["chainmail"].ItemsFromThisItems);

        public override IReadOnlyList<Item> Ingredients { get; } = GetItemsFromStringList(Plugin.Instance.Config.Items["chainmail"].Ingredients);

        public override IReadOnlyDictionary<StatisticsType, Value> Statistics { get; } = Plugin.Instance.Config.Items["bracer"].Statistics;

        public override int Cost { get; } = Plugin.Instance.Config.Items["chainmail"].Cost;

        public override int SellCost { get; } = Plugin.Instance.Config.Items["chainmail"].SellCost;

        public Chainmail() : base() { }

        public Chainmail(Hero owner) : base(owner) { }

        public override Item Create(Hero owner)
        {
            return new Chainmail(owner);
        }
    }
}
