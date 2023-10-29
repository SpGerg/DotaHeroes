using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Items
{
    public class BootsOfSpeed : AutoItem
    {
        public override string Name => "Boots of speed";

        public override string Slug => "boots_of_speed";

        public override string Description => "Boots of speed";

        public override string Lore => "Boots of speed";

        public BootsOfSpeed() : base() { }

        protected BootsOfSpeed(Hero owner) : base(owner)
        {
        }

        public override Item Create(Hero owner)
        {
            return new BootsOfSpeed(owner);
        }

        public override string ToString()
        {
            return $"{Name}: {(MainAbility as ToggleAbility).IsActive}";
        }
    }
}
