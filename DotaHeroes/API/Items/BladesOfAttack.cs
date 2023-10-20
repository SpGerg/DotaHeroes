using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System.Collections.Generic;

namespace DotaHeroes.API.Items
{
    public class BladesOfAttack : Item
    {
        public override string Name => "Blades of attack";

        public override string Slug => "blades_of_attack";

        public override string Description => "Blades of attack";

        public override string Lore => "Blades of attack";

        public override Ability MainAbility { get; } = API.GetAbilityOrDefaultBySlug("blades_of_attack");

        public override List<Ability> Passives { get; } = Ability.ToAbilitiesFromStringList(Plugin.Instance.Config.Items["blades_of_attack"].Passives);

        public override IReadOnlyList<Item> Ingredients => GetItemsFromStringList(Plugin.Instance.Config.Items["blades_of_attack"].Ingredients);

        public override IReadOnlyList<Item> ItemsFromThisItem => GetItemsFromStringList(Plugin.Instance.Config.Items["blades_of_attack"].ItemsFromThisItems);

        public override IReadOnlyDictionary<StatisticsType, Value> Statistics { get; } = Plugin.Instance.Config.Items["helm_of_iron_will"].Statistics;

        public override int Cost { get; } = Plugin.Instance.Config.Items["blades_of_attack"].Cost;

        public override int SellCost { get; } = Plugin.Instance.Config.Items["blades_of_attack"].SellCost;

        public BladesOfAttack() : base()
        {
        }

        protected BladesOfAttack(Hero owner) : base(owner)
        {
        }

        public override Item Create(Hero owner)
        {
            return new BladesOfAttack(owner);
        }
    }
}
