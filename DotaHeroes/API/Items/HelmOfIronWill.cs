using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System.Collections.Generic;

namespace DotaHeroes.API.Items
{
    public class HelmOfIronWill : Item
    {
        public override string Name => "Helm of iron will";

        public override string Slug => "helm_of_iron_will";

        public override string Description => "Helm of iron will";

        public override string Lore => "Helm of iron will";

        public override Ability MainAbility { get; } = API.GetAbilityOrDefaultBySlug("helm_of_iron_will");

        public override List<Ability> Passives { get; } = Ability.ToAbilitiesFromStringList(Plugin.Instance.Config.Items["helm_of_iron_will"].Passives);

        public override IReadOnlyList<Item> Ingredients => GetItemsFromStringList(Plugin.Instance.Config.Items["helm_of_iron_will"].Ingredients);

        public override IReadOnlyList<Item> ItemsFromThisItem => GetItemsFromStringList(Plugin.Instance.Config.Items["helm_of_iron_will"].ItemsFromThisItems);

        public override IReadOnlyDictionary<StatisticsType, Value> Statistics { get; } = Plugin.Instance.Config.Items["helm_of_iron_will"].Statistics;

        public override int Cost { get; } = Plugin.Instance.Config.Items["helm_of_iron_will"].Cost;

        public override int SellCost { get; } = Plugin.Instance.Config.Items["helm_of_iron_will"].SellCost;

        public HelmOfIronWill() : base()
        {
        }

        protected HelmOfIronWill(Hero owner) : base(owner)
        {
        }

        public override Item Create(Hero owner)
        {
            return new HelmOfIronWill(owner);
        }
    }
}
