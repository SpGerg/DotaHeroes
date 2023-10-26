using DotaHeroes.API;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Events.Handlers;
using DotaHeroes.API.Extensions;
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
            var hero = DTAPI.GetRegisteredHeroes().GetRandomValue().Value;
            
            foreach (var role in hero.ChangeRoles)
            {
                Log.Info(role);
            }

            if (hero.ChangeRoles.Contains(ev.NewRole))
            {
                var _hero = player.SetHero(hero);

                if (_hero == default)
                {
                    return;
                }

                _hero.SideType = API.Features.Utils.GetRandomSide();

                Log.Info($"Player {player.Nickname} hero is {hero.HeroName}, side is {_hero.SideType}");

                Hud.Update(_hero);
            }
            else
            {
                player.RemoveHero();
            }
        }

        internal static void RemoveHero(LeftEventArgs ev)
        {
            ev.Player.RemoveHero();
        }
    }
}
