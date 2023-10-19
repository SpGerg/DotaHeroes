﻿using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Items
{
    public class Bracer : Item
    {
        public override string Name => "Bracer";

        public override string Slug => "bracer";

        public override string Description => "Bracer";

        public override string Lore => "Bracer";

        public override Ability MainAbility { get; } = API.GetAbilityOrDefaultBySlug("bracer");

        public override List<Ability> Passives { get; } = Ability.ToAbilitiesFromStringList(Plugin.Instance.Config.Items["bracer"].Passives);

        public override IReadOnlyList<Item> Ingredients => GetItemsFromStringList(Plugin.Instance.Config.Items["bracer"].Ingredients);

        public override IReadOnlyList<Item> ItemsFromThisItem => GetItemsFromStringList(Plugin.Instance.Config.Items["bracer"].ItemsFromThisItems);

        public override IReadOnlyDictionary<StatisticsType, Value> Statistics { get; } = Plugin.Instance.Config.Items["bracer"].Statistics;

        public override int Cost { get; } = Plugin.Instance.Config.Items["bracer"].Cost;

        public override int SellCost { get; } = Plugin.Instance.Config.Items["bracer"].SellCost;

        public Bracer() : base()
        {
        }

        protected Bracer(Hero owner) : base(owner)
        {
        }

        public override Item Create(Hero owner)
        {
            return new Bracer(owner);
        }
    }
}
