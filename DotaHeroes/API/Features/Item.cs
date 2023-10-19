﻿using DotaHeroes.API.Enums;
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

        public abstract string Slug { get; }

        public abstract string Description { get; }

        public abstract string Lore { get; }

        public abstract int Cost { get; }

        public abstract int SellCost { get; }

        public virtual Ability MainAbility { get; }

        public virtual List<Ability> Passives { get; }

        public virtual IReadOnlyDictionary<StatisticsType, Value> Statistics { get; }

        public virtual IReadOnlyList<Item> Ingredients { get; }

        public virtual IReadOnlyList<Item> ItemsFromThisItem { get; }

        public Hero Owner { get; }

        /// <summary>
        /// On added (buyed)
        /// </summary>
        public virtual void Added() { }

        /// <summary>
        /// On removed
        /// </summary>
        public virtual void Removed() { }

        /// <summary>
        /// On selled
        /// </summary>
        public virtual void Selled() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item" /> class.
        /// </summary>
        public Item()
        {
            Owner = null;
            MainAbility = null;
            Passives = new List<Ability>();
            Ingredients = new List<Item>();
            Statistics = new Dictionary<StatisticsType, Value>();
            ItemsFromThisItem = new List<Item>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item" /> class.
        /// </summary>
        /// <param name="owner"><inheritdoc cref="Owner" /></param>
        protected Item(Hero owner) : this()
        {
            Owner = owner;
        }

        public static List<Item> GetItemsFromStringList(List<string> items)
        {
            var result = new List<Item>();

            foreach (var item in items)
            {
                var _item = API.GetItemOrDefaultBySlug(item);

                if (_item == default) continue;

                result.Add(_item);
            }

            return result;
        }

        public abstract Item Create(Hero owner);
    }
}
