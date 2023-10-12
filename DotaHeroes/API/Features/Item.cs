using DotaHeroes.API.Enums;
using DotaHeroes.API.Interfaces;
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
        public abstract string Name { get; }

        public abstract string Description { get; }

        public abstract string Lore { get; }

        public virtual Ability MainAbility { get; }

        public virtual List<PassiveAbility> Passives { get; }

        public virtual IReadOnlyDictionary<StatisticsType, Value> Statistics { get; }

        public Player Owner { get; }

        /// <summary>
        /// On added (buyed)
        /// </summary>
        public virtual void Added() { }

        /// <summary>
        /// On removed (selled)
        /// </summary>
        public virtual void Removed() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item" /> class.
        /// </summary>
        /// <param name="owner"><inheritdoc cref="Owner" /></param>
        public Item(Player owner)
        {
            Owner = owner;
        }
    }
}
