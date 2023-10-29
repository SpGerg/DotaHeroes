using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Items
{
    public class Broadsword : AutoItem
    {
        public override string Name => "Broadsword";

        public override string Slug => "broadsword";

        public override string Description => "Broadsword";

        public override string Lore => "Broadsword";

        public Broadsword() : base() { }

        protected Broadsword(Hero owner) : base(owner)
        {
        }

        public override Item Create(Hero owner)
        {
            return new Broadsword(owner);
        }
    }
}
