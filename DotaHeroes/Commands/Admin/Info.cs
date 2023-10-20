using DotaHeroes.API.Features;
using Exiled.API.Features;
using System;

namespace DotaHeroes.Commands.Admin
{
    public class Info : AdminCommandBase
    {
        public override string Command => "info";

        public override string Description => "Info about hero";

        public override bool Execute(Player player, ArraySegment<string> arguments, out string response)
        {
            if (!API.Features.Utils.GetHeroFromPlayerEyeDirection(player, 5, out response, out Hero target))
            {
                return false;
            }

            response = target.ToString();
            return true;
        }
    }
}
