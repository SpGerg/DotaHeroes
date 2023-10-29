using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Items
{
    public class Buckler : AutoItem
    {
        public override string Name => "Buckler";

        public override string Slug => "buckler";

        public override string Description => "Buckler";

        public override string Lore => "Buckler";

        public Buckler() : base()
        {
        }

        protected Buckler(Hero owner) : base(owner)
        {
        }

        public override Item Create(Hero owner)
        {
            return new Buckler(owner);
        }
    }
}
