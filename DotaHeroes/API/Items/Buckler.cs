using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Items
{
    public class Buckler : Item
    {
        public override string Name => "Buckler";

        public override string Slug => "buckler";

        public override string Description => "Buckler";

        public override string Lore => "Buckler";

        public override Ability MainAbility { get; } = DTAPI.GetAbilityOrDefaultBySlug(Plugin.Instance.Config.Items["buckler"].Ability);

        public override List<Ability> Passives { get; } = Ability.ToAbilitiesFromStringList(Plugin.Instance.Config.Items["buckler"].Passives);

        public override IReadOnlyList<Item> Ingredients { get; } = GetItemsFromStringList(Plugin.Instance.Config.Items["buckler"].Ingredients);

        public override IReadOnlyList<Item> ItemsFromThisItem { get; } = GetItemsFromStringList(Plugin.Instance.Config.Items["buckler"].ItemsFromThisItems);

        public override IReadOnlyDictionary<StatisticsType, Value> Statistics { get; } = Plugin.Instance.Config.Items["buckler"].Statistics;

        public override int Cost { get; } = Plugin.Instance.Config.Items["buckler"].Cost;

        public override int SellCost { get; } = Plugin.Instance.Config.Items["buckler"].SellCost;

        public Buckler() { }

        protected Buckler(Hero owner) : base(owner) { }

        public override Item Create(Hero owner)
        {
            return new Buckler(owner);
        }
    }
}
