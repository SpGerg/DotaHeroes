﻿using DotaHeroes.API.Enums;
using Exiled.API.Features.Pools;
using System;
using System.Collections.Generic;

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
                var stringBuilder = StringBuilderPool.Pool.Get();

                stringBuilder.AppendLine("Command format: buy_item <name>");
                stringBuilder.AppendLine("Command example: buy_item bracer");

                stringBuilder.AppendLine("Item list: ");

                foreach (var item in API.DTAPI.GetRegisteredItems())
                {
                    stringBuilder.AppendLine(item.Value.Slug);
                }

                response = StringBuilderPool.Pool.ToStringReturn(stringBuilder);
                return true;
            }

            var _item = API.DTAPI.GetItemOrDefaultBySlug(arguments.Array[2]);

            if (_item == default)
            {
                var stringBuilder = StringBuilderPool.Pool.Get();

                stringBuilder.AppendLine("Item list: ");

                foreach (var item in API.DTAPI.GetRegisteredItems())
                {
                    stringBuilder.AppendLine(item.Value.Slug);
                }

                response = StringBuilderPool.Pool.ToStringReturn(stringBuilder);
                return true;
            }

            if (hero.Money - _item.Cost < 0)
            {
                response = "You havent money for this!";
                return true;
            }

            hero.Money -= _item.Cost;

            if (!hero.Inventory.AddItem(_item))
            {
                response = "Your inventory is full";
                return false;
            }

            response = $"Item {_item.Name} was buyed";
            return true;
        }
    }
}
