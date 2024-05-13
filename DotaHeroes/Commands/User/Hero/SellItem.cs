using Exiled.API.Features.Pools;
using System;

namespace DotaHeroes.Commands.User.Hero
{
    public class SellItem : HeroCommandBase
    {
        public override string Command => "sell_item";

        public override string Description => "Selling item";

        protected override bool Execute(API.Features.Hero hero, ArraySegment<string> arguments, out string response)
        {
            if (arguments.Count == 0)
            {
                var stringBuilder = StringBuilderPool.Pool.Get();

                stringBuilder.AppendLine("Command format: sell_item <index>");
                stringBuilder.AppendLine("Command example: sell_item 0");
                stringBuilder.AppendLine("Get item index you can by .inventory command");

                response = StringBuilderPool.Pool.ToStringReturn(stringBuilder);
                return true;
            }

            if (!int.TryParse(arguments.Array[2], out int result))
            {
                response = "First argument is not number";
                return true;
            }

            var item = hero.Inventory.GetItem(result);

            if (item == default)
            {
                response = $"Item with {result} index not found";
                return true;
            }

            hero.Money += item.SellCost;

            hero.Inventory.RemoveItem(item, true);

            response = item.Name + " was selled";
            return true;
        }
    }
}
