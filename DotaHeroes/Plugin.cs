using DotaHeroes.Events.Internal;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Player = Exiled.Events.Handlers.Player;

namespace DotaHeroes
{
    public class Plugin : Plugin<Config>
    {
        public override void OnEnabled()
        {
            Player.Spawned += PlayerHandler.OnSpawned;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Player.Spawned -= PlayerHandler.OnSpawned;

            base.OnDisabled();
        }
    }
}
