using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Items
{
    public class RingOfProtection : AutoItem
    {
        public override string Name => "Ring of protection";

        public override string Slug => "ring_of_protection";

        public override string Description => "Ring of protection";

        public override string Lore => "Ring of protection";

        public RingOfProtection() : base()
        {
            
        }

        protected RingOfProtection(Hero owner) : base(owner) { }

        public override Item Create(Hero owner)
        {
            return new RingOfProtection(owner);
        }
    }
}
