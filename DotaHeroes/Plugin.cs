using DotaHeroes.API;
using DotaHeroes.API.Features;
using DotaHeroes.Events.Internal;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Player = Exiled.Events.Handlers.Player;
using Hero = DotaHeroes.API.Events.Handlers.Hero;

namespace DotaHeroes
{
    public class Plugin : Plugin<Config>
    {
        public override void OnEnabled()
        {
            Hud.RunUpdate();

            Player.Spawned += PlayerHandler.OnSpawned;
            Hero.TakedDamage += HeroHandler.OnHeroTakedDamage;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Player.Spawned -= PlayerHandler.OnSpawned;

            base.OnDisabled();
        }
    }
}
