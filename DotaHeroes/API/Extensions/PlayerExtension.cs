using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Features.Components;
using Exiled.API.Features;
using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
            player.IsGodModeEnabled = true;

            return createdHero;
        }

        public static bool RemoveHero(this Player player)
        {
            HeroController heroController = player.GameObject.GetComponent<HeroController>();
            
            if (heroController == null)
            {
                return false;
            }

            GameObject.Destroy(heroController);

            API.SetOrAddPlayer(player.Id, default);

            return true;
        }
    }
}
