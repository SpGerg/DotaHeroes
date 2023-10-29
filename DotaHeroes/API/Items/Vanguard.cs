using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Items
{
    public class Vanguard : AutoItem
    {
        public override string Name => "Vanguard";

        public override string Slug => "vanguard";

        public override string Description => "Vanguard";

        public override string Lore => "Vanguard";

        public Vanguard() : base()
        {
            
        }

        protected Vanguard(Hero owner) : base(owner)
        {
            
        }

        public override Item Create(Hero owner)
        {
            return new Vanguard(owner);
        }
    }
}
