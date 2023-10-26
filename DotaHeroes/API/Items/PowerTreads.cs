using DotaHeroes.API.Abilities.Items;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using NorthwoodLib.Pools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Items
{
    public class PowerTreads : Item
    {
        public override string Name => "Powet treads";

        public override string Slug => "power_treads";

        public override string Description => "Powet treads";

        public override string Lore => "Powet treads";

        public override Ability MainAbility { get; } = DTAPI.GetAbilityOrDefaultBySlug(Plugin.Instance.Config.Items["power_treads"].Ability);

        public override int Cost { get; } = Plugin.Instance.Config.Items["power_treads"].Cost;

        public override int SellCost { get; } = Plugin.Instance.Config.Items["power_treads"].SellCost;

        public PowerTreads() : base() { }

        protected PowerTreads(Hero owner) : base(owner) { }

        public override void Added()
        {
            if (MainAbility is SwitchAttribute switchAttribute)
            {
                switchAttribute.UpdateAttribute(Owner, Owner.HeroStatistics.AttributeType);
            }

            base.Added();
        }

        public override void Removed()
        {
            if (MainAbility is SwitchAttribute switchAttribute)
            {
                switchAttribute.UpdateAttribute(Owner, AttributeType.None);
            }

            base.Removed();
        }

        public override void Selled()
        {
            Removed();

            base.Selled();
        }

        public override string ToStringHud(Hero hero)
        {
            return MainAbility?.ToStringHud(hero);
        }

        public override Item Create(Hero owner)
        {
            return new PowerTreads(owner);
        }
    }
}
