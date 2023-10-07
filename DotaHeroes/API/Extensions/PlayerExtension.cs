using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Features.Components;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Extensions
{
    public static class PlayerExtension
    {
        public static Hero SetHero<T>(this Player player) where T : Hero, new()
        {
            var createdHero = new T().Create(player, SideType.Dire);

            HeroController heroController = player.GameObject.AddComponent<HeroController>();
            heroController.Hero = createdHero;
            createdHero.Respawn();

            return createdHero;
        }

        public static Hero SetHero(this Player player, Hero hero)
        {
            var createdHero = hero.Create(player, SideType.Dire);

            HeroController heroController = player.GameObject.AddComponent<HeroController>();
            heroController.Hero = createdHero;
            createdHero.Respawn();

            return createdHero;
        }
    }
}
