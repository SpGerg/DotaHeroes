using DotaHeroes.API.Enums;
using NorthwoodLib.Pools;
using System.Collections.Generic;
using System.Text;

namespace DotaHeroes.API.Features
{
    public abstract class Item
    {
        public abstract string Name { get; }

        public abstract string Slug { get; }

        public abstract string Description { get; }

        public abstract string Lore { get; }

        public abstract int Cost { get; protected set; }

        public abstract int SellCost { get; protected set; }

        public virtual Ability MainAbility { get; set; }

        public virtual List<Ability> Passives { get; protected set; }

        public virtual IReadOnlyDictionary<StatisticsType, Value> Statistics { get; protected set; }

        public virtual IReadOnlyList<Item> Ingredients { get; protected set; }

        public virtual IReadOnlyList<Item> ItemsFromThisItem { get; protected set; }

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

        public static List<Item> GetItemsFromStringList(Hero hero, List<string> items, bool isCreate = false)
        {
            var result = new List<Item>();

            foreach (var item in items)
            {
                var _item = DTAPI.GetItemOrDefaultBySlug(item);

                if (_item == default) continue;

                result.Add(isCreate ? _item.Create(hero) : _item);
            }

            return result;
        }

        /// <summary>
        /// To string hud.
        /// </summary>
        public virtual string ToStringHud(Hero hero)
        {
            StringBuilder stringBuilder = StringBuilderPool.Shared.Rent();
            stringBuilder.AppendLine("Name: " + Name);

            var cooldown = Cooldowns.GetCooldown(hero.Player.Id, Slug);

            if (cooldown != default)
            {
                stringBuilder.AppendLine("Cooldown: " + cooldown.Cooldown);
            }

            if (MainAbility != null && MainAbility is ToggleAbility toggleAbility)
            {
                stringBuilder.AppendLine("Active: " + toggleAbility.ToStringIsActive());
            }

            return StringBuilderPool.Shared.ToStringReturn(stringBuilder);
        }

        public abstract Item Create(Hero owner);
    }
}
