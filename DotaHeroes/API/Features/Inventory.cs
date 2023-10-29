using DotaHeroes.API.Interfaces;
using Exiled.API.Features;
using System.Collections.Generic;
using System.Linq;
using Utils.NonAllocLINQ;

namespace DotaHeroes.API.Features
{
    public class Inventory
    {
        public Hero Owner { get; }

        public int MaxItems { get; }

        protected List<Item> Items { get; }

        public Inventory(Hero owner, int maxItems = 5)
        {
            Owner = owner;
            Items = new List<Item>();
            MaxItems = maxItems;
        }

        /// <summary>
        /// Add items.
        /// </summary>
        public void AddItems(IReadOnlyList<Item> items, bool isJustAdd = false)
        {
            foreach (var item in items)
            {
                AddItem(item, isJustAdd);
            }
        }

        /// <summary>
        /// Add item.
        /// </summary>
        public bool AddItem(Item item, bool isJustAdd = false)
        {
            if (Items.Count >= MaxItems) return false;

            Owner.HeroStatistics.AddOrReduceStatistics(item.Statistics, false);

            Utils.AddModifier(Owner, item as IModifier);

            foreach (var passive in item.Passives.ToList())
            {
                var _passive = passive.Create(Owner);
                item.Passives.Remove(passive);
                item.Passives.Add(_passive);

                if (_passive is PassiveAbility passiveAbility)
                {
                    passiveAbility.Register();
                }

                if (_passive is Aura aura)
                {
                    aura.IsActive = true;
                }
            }

            if (!isJustAdd)
            {
                Create(item);
            }
            else
            {
                item.Added();
                Items.Add(item);
            }

            Hud.Update(item.Owner);

            return true;
        }

        /// <summary>
        /// Add item.
        /// </summary>
        public bool AddItem<T>(bool isJustAdd = false) where T : Item, new()
        {
            return AddItem(new T(), isJustAdd);
        }

        /// <summary>
        /// Add item.
        /// </summary>
        public bool ExecuteItem(Item item)
        {
            if (item == null || item.MainAbility == null) return false;

            var cooldown = Cooldowns.GetCooldown(Owner.Player.Id, item.Slug);

            if (cooldown != default)
            {
                if (!cooldown.IsReady) return false;
            }

            string response = string.Empty;

            Owner.ExecuteAbility(item.MainAbility.Slug, ref response, true);

            Owner.Player.SendConsoleMessage(response, "default");

            return true;
        }

        /// <summary>
        /// Add item.
        /// </summary>
        public bool ExecuteItem(int index)
        {
            return ExecuteItem(GetItem(index));
        }

        /// <summary>
        /// Add item.
        /// </summary>
        public void ExecuteItem<T>() where T : Item, new()
        {
            var item = Items.FirstOrDefault(_item => ContainsBySlug(_item));

            if (item == default) return;

            ExecuteItem(item);
        }

        /// <summary>
        /// Remove item.
        /// </summary>
        public void RemoveItem(Item item, bool isSelled = false)
        {
            Owner.HeroStatistics.AddOrReduceStatistics(item.Statistics, true);

            Utils.RemoveModifier(Owner, item as IModifier);

            foreach (var passive in item.Passives)
            {
                if (passive is PassiveAbility passiveAbility)
                {
                    passiveAbility.Unregister();
                }

                if (passive is Aura aura)
                {
                    aura.IsActive = false;
                }
            }

            if (isSelled)
            {
                item.Selled();
            }
            else
            {
                item.Removed();
            }

            Items.Remove(item);

            Hud.Update(Owner);
        }

        /// <summary>
        /// Remove item.
        /// </summary>
        public void RemoveItem<T>(bool isSelled = false) where T : Item, new()
        {
            RemoveItem(new T(), isSelled);
        }

        /// <summary>
        /// Get item by index.
        /// </summary>
        public Item GetItem(int index)
        {
            if (index > Items.Count) return default;

            return Items[index];
        }

        /// <summary>
        /// Return new copy of items
        /// </summary>
        public List<Item> GetItems()
        {
            return Items;
        }

        //very not optimized
        private void Create(Item item)
        {
            item.Added();
            Items.Add(item);

            if (item.ItemsFromThisItem.IsEmpty())
            {
                return;
            }

            foreach (var _item in item.ItemsFromThisItem)
            {
                if (_item.Ingredients.ToList().TrueForAll(ContainsBySlug))
                {
                    foreach (var ingredient in Items.ToList())
                    {
                        if (ContainsBySlug((List<Item>)_item.Ingredients, ingredient))
                        {
                            RemoveItem(ingredient);
                        }
                    }

                    AddItem(_item);
                    return;
                }
            }
        }

        private bool ContainsBySlug(Item item)
        {
            return Items.Find(_item => _item.Slug == item.Slug) != default;
        }

        private bool ContainsBySlug(List<Item> items, Item item)
        {
            return items.Find(_item => _item.Slug == item.Slug) != default;
        }
    }
}
