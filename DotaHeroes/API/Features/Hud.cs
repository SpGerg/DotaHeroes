using DotaHeroes.API.Enums;
using Exiled.API.Features;
using MEC;
using NorthwoodLib.Pools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DotaHeroes.API.Features
{
    public static class Hud
    {
        /// <summary>
        /// Update hud to all heroes.
        /// </summary>
        public static void Update()
        {
            foreach (var hero in API.GetHeroes().Values)
            {
                Update(hero);
            }
        }

        /// <summary>
        /// Update hud to hero.
        /// </summary>
        public static void Update(Hero hero)
        {
            if (hero.IsHeroDead) return;

            var player = hero.Player;
            var abilites = StringBuilderPool.Shared.Rent();

            foreach (var ability in hero.Abilities)
            {
                abilites.AppendLine(ability.Name);
                abilites.AppendLine("Cooldown: " + Cooldowns.ToStringIsCooldown(hero.Player.Id, ability.Name));
            }

            player.ShowHint($"<size=16><align=Left>{hero}</align></size><size=12><align=Right>{StringBuilderPool.Shared.ToStringReturn(abilites)}</align></size>", short.MaxValue);
        }
    }
}
