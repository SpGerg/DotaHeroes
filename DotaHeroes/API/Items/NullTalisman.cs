using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System.Collections.Generic;

namespace DotaHeroes.API.Items
{
    public class NullTalisman : AutoItem
    {
        public override string Name => "Null talisman";

        public override string Slug => "null_talisman";

        public override string Description => "Nulli";

        public override string Lore => "talisman is talisman";

        public NullTalisman() : base()
        {
        }

        protected NullTalisman(Hero owner) : base(owner)
        {
        }

        public override Item Create(Hero owner)
        {
            return new NullTalisman(owner);
        }
    }
}
