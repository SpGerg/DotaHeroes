using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Features
{
    public abstract class Item
    {
        public string Name { get; }

        public string Description { get; }

        public string Lore { get; }

        public virtual List<Ability> Abilities { get; } = new List<Ability>();

        public Player Owner { get; }

        public Item(Player owner)
        {
            Owner = owner;
        }
    }
}
