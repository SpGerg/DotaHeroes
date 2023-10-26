﻿using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System.Collections.Generic;

namespace DotaHeroes.API.Items
{
    public class SlippersOfAgility : Item
    {
        public override string Name => "Slippers Of Agility";

        public override string Slug => "slippers_of_agility";

        public override string Description => "slippers_of_agility";

        public override string Lore => "slippers_of_agility is slippers_of_agility";

        public override Ability MainAbility { get; } = DTAPI.GetAbilityOrDefaultBySlug(Plugin.Instance.Config.Items["slippers_of_agility"].Ability);

        public override List<Ability> Passives { get; } = Ability.ToAbilitiesFromStringList(Plugin.Instance.Config.Items["slippers_of_agility"].Passives);

        public override IReadOnlyList<Item> Ingredients { get; } = GetItemsFromStringList(Plugin.Instance.Config.Items["slippers_of_agility"].Ingredients);

        public override IReadOnlyList<Item> ItemsFromThisItem { get; } = GetItemsFromStringList(Plugin.Instance.Config.Items["slippers_of_agility"].ItemsFromThisItems);

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
