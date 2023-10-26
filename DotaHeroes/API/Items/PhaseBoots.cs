using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Items
{
    public class PhaseBoots : Item
    {
        public override string Name => "Phase boots";

        public override string Slug => "phase_boots";

        public override string Description => "Phase boots";

        public override string Lore => "Kialidl lol kok ik l";

        public override Ability MainAbility { get; } = DTAPI.GetAbilityOrDefaultBySlug(Plugin.Instance.Config.Items["phase_boots"].Ability);

        public override List<Ability> Passives { get; } = Ability.ToAbilitiesFromStringList(Plugin.Instance.Config.Items["phase_boots"].Passives);

        public override IReadOnlyList<Item> Ingredients { get; } = GetItemsFromStringList(Plugin.Instance.Config.Items["phase_boots"].Ingredients);

        public override IReadOnlyList<Item> ItemsFromThisItem { get; } = GetItemsFromStringList(Plugin.Instance.Config.Items["phase_boots"].ItemsFromThisItems);

        public override IReadOnlyDictionary<StatisticsType, Value> Statistics { get; } = Plugin.Instance.Config.Items["phase_boots"].Statistics;

        public override int Cost { get; } = Plugin.Instance.Config.Items["phase_boots"].Cost;

        public override int SellCost { get; } = Plugin.Instance.Config.Items["phase_boots"].SellCost;

        public PhaseBoots() : base() { }

        protected PhaseBoots(Hero owner) : base(owner) { }

        public override Item Create(Hero owner)
        {
            return new PhaseBoots(owner);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
