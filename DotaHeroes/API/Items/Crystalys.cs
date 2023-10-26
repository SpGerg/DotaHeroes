using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Items
{
    public class Crystalys : Item
    {
        public override string Name => "Crystalys";

        public override string Slug => "crystalys";

        public override string Description => "Crystalys";

        public override string Lore => "Crystalys";

        public override Ability MainAbility { get; } = DTAPI.GetAbilityOrDefaultBySlug(Plugin.Instance.Config.Items["crystalys"].Ability);

        public override List<Ability> Passives { get; } = Ability.ToAbilitiesFromStringList(Plugin.Instance.Config.Items["crystalys"].Passives);

        public override IReadOnlyList<Item> Ingredients { get; } = GetItemsFromStringList(Plugin.Instance.Config.Items["crystalys"].Ingredients);

        public override IReadOnlyList<Item> ItemsFromThisItem { get; } = GetItemsFromStringList(Plugin.Instance.Config.Items["crystalys"].ItemsFromThisItems);

        public override IReadOnlyDictionary<StatisticsType, Value> Statistics { get; } = Plugin.Instance.Config.Items["crystalys"].Statistics;

        public override int Cost { get; } = Plugin.Instance.Config.Items["crystalys"].Cost;

        public override int SellCost { get; } = Plugin.Instance.Config.Items["crystalys"].SellCost;

        public Crystalys() { }

        protected Crystalys(Hero owner) : base(owner) { }

        public override Item Create(Hero owner)
        {
            return new Crystalys(owner);
        }
    }
}
