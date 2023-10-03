using DotaHeroes.API;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Features.Components;
using DotaHeroes.API.Heroes;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Roles;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DotaHeroes.Events.Internal
{
    internal static class PlayerHandler
    {
        internal static void SetHero(ChangingRoleEventArgs ev)
        {
            var player = ev.Player;
            var hero = API.API.GetRegisteredHeroes().GetRandomValue().Value;

            if (ev.NewRole is RoleTypeId.Spectator && hero != default)
            {
                GameObject.Destroy(hero.HeroController);
                return;
            }

            if (!hero.ChangeRoles.Contains(ev.NewRole))
            {
                return;
            }

            var createdHero = hero.Create(player, SideType.Dire);

            ev.NewRole = createdHero.Model;
            HeroController heroController = player.GameObject.AddComponent<HeroController>();
            heroController.Hero = createdHero;
            createdHero.Respawn();

            Log.Info($"Player {player.Nickname} hero is {hero.HeroName}");

            Hud.Update(createdHero);
        }
    }
}
