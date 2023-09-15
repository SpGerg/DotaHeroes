using DotaHeroes.API;
using DotaHeroes.API.Features.Components;
using DotaHeroes.API.Heroes;
using Exiled.Events.EventArgs.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.Events.Internal
{
    internal static class PlayerHandler
    {
        internal static void OnSpawned(SpawnedEventArgs ev)
        {
            var heroController = ev.Player.GameObject.AddComponent<HeroController>();
            heroController.Hero = new Pudge(ev.Player);
        }
    }
}
