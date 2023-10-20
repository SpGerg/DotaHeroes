using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Features.Components;
using Exiled.API.Features;
using UnityEngine;

namespace DotaHeroes.API.Extensions
{
    public static class PlayerExtension
    {
        public static Hero SetHero<T>(this Player player) where T : Hero, new()
        {
            var hero = SetHero(player, new T());

            return hero;
        }

        public static Hero SetHero(this Player player, Hero hero)
        {
            if (player.IsAudio())
            {
                return default;
            }

            //RemoveHero(player);

            var createdHero = hero.Create(player, SideType.Dire);

            HeroController heroController = player.GameObject.AddComponent<HeroController>();
            heroController.Hero = createdHero;
            createdHero.Respawn();
            player.IsGodModeEnabled = true;

            return createdHero;
        }

        public static bool RemoveHero(this Player player)
        {
            if (player.IsAudio())
            {
                return default;
            }

            HeroController heroController = player.GameObject.GetComponent<HeroController>();
            
            if (heroController == null)
            {
                return false;
            }

            foreach (var ability in heroController.Hero.Abilities)
            {
                if (ability is PassiveAbility passiveAbility)
                {
                    passiveAbility.Unregister(heroController.Hero);
                }
            }

            GameObject.Destroy(heroController);

            API.SetOrAddPlayer(player.Id, default);

            Hud.Clear(player);

            return true;
        }

        public static bool IsAudio(this Player player)
        {
            if (player.Nickname.Contains(Audio.SoundBotName) || player.CustomName.Contains(Audio.SoundBotName))
            {
                return true;
            }

            return false;
        }
    }
}
