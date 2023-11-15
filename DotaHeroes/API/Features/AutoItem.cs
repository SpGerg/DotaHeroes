using DotaHeroes.API.Features.Serializables;
using Exiled.API.Features;
using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Features
{
    public abstract class AutoItem : Item
    {
        public override int Cost { get; protected set; }

        public override int SellCost { get; protected set; }

        public AutoItem() : base()
        {
            SetValues(null, false);
        }

        public AutoItem(Hero hero) : base(hero)
        {
            SetValues(hero, true);
        }

        private void SetValues(Hero hero, bool isCreate)
        {
            if (!isCreate)
            {
                MainAbility = DTAPI.GetAbilityOrDefaultBySlug(Plugin.Instance.Config.Items[Slug].Ability);
            }
            else
            {
                MainAbility = DTAPI.GetAbilityOrDefaultBySlug(Plugin.Instance.Config.Items[Slug].Ability)?.Create(hero);
            }

            Passives = Ability.ToAbilitiesFromStringList(hero, Plugin.Instance.Config.Items[Slug].Passives, isCreate);

            Ingredients = GetItemsFromStringList(hero, Plugin.Instance.Config.Items[Slug].Ingredients);

            ItemsFromThisItem = GetItemsFromStringList(hero, Plugin.Instance.Config.Items[Slug].ItemsFromThisItems);

            Statistics = Plugin.Instance.Config.Items[Slug].Statistics;

            Cost = Plugin.Instance.Config.Items[Slug].Cost;

            SellCost = Plugin.Instance.Config.Items[Slug].SellCost;
        }
    }
}
