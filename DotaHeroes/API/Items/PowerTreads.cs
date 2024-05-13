using DotaHeroes.API.Abilities.Items;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;

namespace DotaHeroes.API.Items
{
    public class PowerTreads : AutoItem
    {
        public override string Name => "Powet treads";

        public override string Slug => "power_treads";

        public override string Description => "Powet treads";

        public override string Lore => "Powet treads";

        private SwitchAttribute _switchAttribute;

        public PowerTreads() : base()
        {
            if (MainAbility is SwitchAttribute switchAttribute)
            {
                _switchAttribute = switchAttribute;
            }
        }

        protected PowerTreads(Hero owner) : base(owner)
        {
        }

        public override void Added()
        {
            _switchAttribute.UpdateAttribute(Owner, Owner.HeroStatistics.AttributeType);

            base.Added();
        }

        public override void Removed()
        {
            _switchAttribute.UpdateAttribute(Owner, AttributeType.None);

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
