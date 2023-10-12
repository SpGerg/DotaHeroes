using CommandSystem;
using DotaHeroes.API.Extensions;
using DotaHeroes.API.Features.Components;
using Exiled.API.Extensions;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.Commands.Admin
{
    public class HeroDummy : AdminCommandBase
    {
        public override string Command => "hero_dummy";

        public override string Description => "Create a hero dummy";

        public override bool Execute(Player player, ArraySegment<string> arguments, out string response)
        {
            var npc = Npc.Spawn($"Hero Dummy", PlayerRoles.RoleTypeId.Tutorial, 0, string.Empty, player.Position);
            var hero = npc.SetHero(API.API.GetRegisteredHeroes().GetRandomValue().Value);

            response = $"Have been created with {hero.HeroName} hero";
            return true;
        }
    }
}
