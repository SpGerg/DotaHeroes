using DotaHeroes.API.Enums;
using Exiled.API.Features;
using NorthwoodLib.Pools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.Commands.User.Hero
{
    public class BuyItem : HeroCommandBase
    {
        public override string Command => "buy_item";

        public override string Description => "Buying item";

        private static List<AbilityType> IgnoreTypes = new List<AbilityType> { AbilityType.Active, AbilityType.Toggle };

        protected override bool Execute(API.Features.Hero hero, ArraySegment<string> arguments, out string response)
        {
            if (arguments.Count == 0)
            {
                var stringBuilder = StringBuilderPool.Shared.Rent();

                stringBuilder.AppendLine("Command format: buy_item <name>");
                stringBuilder.AppendLine("Command example: buy_item bracer");

                stringBuilder.AppendLine("Item list: ");

                foreach (var item in API.API.GetRegisteredItems())
                {
                    stringBuilder.AppendLine(item.Value.Slug);
                }

                response = StringBuilderPool.Shared.ToStringReturn(stringBuilder);
                return true;
            }

            var _item = API.API.GetItemOrDefaultBySlug(arguments.Array[2]);

            if (_item == default)
            {
                var stringBuilder = StringBuilderPool.Shared.Rent();

                stringBuilder.AppendLine("Item list: ");

                foreach (var item in API.API.GetRegisteredItems())
                {
                    stringBuilder.AppendLine(item.Value.Slug);
                }

                response = StringBuilderPool.Shared.ToStringReturn(stringBuilder);
                return true;
            }

            if (hero.Money - _item.Cost < 0)
            {
                response = "You havent money for this!";
                return true;
            }

            hero.Money -= _item.Cost;
            var buyedItem = _item.Create(hero);

            hero.Inventory.AddItem(buyedItem);

            response = $"Item {buyedItem.Name} was buyed";
            return true;
        }
    }
}
