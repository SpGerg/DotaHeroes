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

        public override Ability MainAbility { get; } = API.GetAbilityOrDefaultBySlug(Plugin.Instance.Config.Items["armlet_of_mordiggian"].Ability);

        public override IReadOnlyDictionary<StatisticsType, Value> Statistics { get; } = Plugin.Instance.Config.Items["armlet_of_mordiggian"].Statistics;

        public override int Cost => 500;

        public override int SellCost => 250;

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
