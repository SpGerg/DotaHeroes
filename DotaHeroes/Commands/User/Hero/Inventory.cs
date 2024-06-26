﻿using Exiled.API.Features.Pools;
using System;

namespace DotaHeroes.Commands.User.Hero
{
    public class Inventory : HeroCommandBase
    {
        public override string Command => "inventory";

        public override string Description => "Show indexes and items in inventory";

        protected override bool Execute(API.Features.Hero hero, ArraySegment<string> arguments, out string response)
        {
            var stringBuilder = StringBuilderPool.Pool.Get();

            var index = -1;

            foreach (var item in hero.Inventory.GetItems())
            {
                stringBuilder.AppendLine($"{index}: {item.Name}");
                stringBuilder.AppendLine($"- {item.Cost}");
                index++;
            }

            response = StringBuilderPool.Pool.ToStringReturn(stringBuilder);
            return true;
        }
    }
}
