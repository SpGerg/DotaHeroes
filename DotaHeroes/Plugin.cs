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
using DotaHeroes.API.Heroes;

namespace DotaHeroes
{
    public class Plugin : Plugin<Config>
    {
        public override void OnEnabled()
        {
            API.API.RegisterHero(new Pudge());

            Player.ChangingRole += PlayerHandler.SetHero;
            Hero.TakingDamage += HeroHandler.BlockingDamage;
            Hero.TakedDamage += HeroHandler.UpdateHudOnTakedDamage;
            Hero.Died += HeroHandler.AddFleshHeapStackOnDied;
            Hero.Healed += HeroHandler.UpdateHudOnHealed;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Player.ChangingRole -= PlayerHandler.SetHero;
            Hero.TakingDamage -= HeroHandler.BlockingDamage;
            Hero.TakedDamage -= HeroHandler.UpdateHudOnTakedDamage;
            Hero.Died -= HeroHandler.AddFleshHeapStackOnDied;
            Hero.Healed -= HeroHandler.UpdateHudOnHealed;

            base.OnDisabled();
        }
    }
}
