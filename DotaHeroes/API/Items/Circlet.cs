using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System.Collections.Generic;

namespace DotaHeroes.API.Items
{
    public class Circlet : AutoItem
    {
        public override string Name => "Circlet";

        public override string Slug => "circlet";

        public override string Description => "circlet";

        public override string Lore => "circlet is circlet";

        public Circlet() : base() { }

        protected Circlet(Hero owner) : base(owner) 
        {
        }

        public override Item Create(Hero owner)
        {
            return new Circlet(owner);
        }
    }
}
