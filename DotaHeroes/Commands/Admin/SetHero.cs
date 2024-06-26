﻿using DotaHeroes.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Pools;
using System;

namespace DotaHeroes.Commands.Admin
{
    public class SetHero : AdminCommandBase
    {
        public override string Command => "set_hero";

        public override string Description => "Set hero";

        public override bool Execute(Player player, ArraySegment<string> arguments, out string response)
        {
            if (arguments.Count == 0)
            {
                var stringBuilder = StringBuilderPool.Pool.Get();

                stringBuilder.AppendLine("Command format: set_hero <hero name>");

                stringBuilder.AppendLine("Hero names list: ");

                foreach (var hero in API.DTAPI.GetRegisteredHeroes())
                {
                    stringBuilder.AppendLine(hero.Value.Slug);
                }

                response = StringBuilderPool.Pool.ToStringReturn(stringBuilder);
                return false;
            }

            var registeredHero = API.DTAPI.GetRegisteredHeroOrDefaultBySlug(arguments.Array[2]);

            if (registeredHero == default)
            {
                response = $"Hero with name {arguments.Array[2]} not found";
                return false;
            }

            if (!API.Features.Utils.GetPlayerFromEyeDirection(player, 5, out response, out Player target))
            {
                return false;
            }

            target.SetHero(registeredHero);

            response = $"Target hero is {registeredHero.HeroName}";
            return true;
        }
    }
}
