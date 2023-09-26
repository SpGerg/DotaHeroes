using DotaHeroes.API.Enums;
using Exiled.API.Features;
using MEC;
using NorthwoodLib.Pools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Features
{
    public static class Hud
    {
        public static void Update()
        {
            foreach (var hero in API.GetHeroes().Values)
            {
                if (hero.IsHeroDead) continue;

                var player = hero.Player;
                var abilites = StringBuilderPool.Shared.Rent();

                foreach (var ability in hero.Abilities)
                {
                    abilites.AppendLine(ability.ToString(hero));
                }

                player.ShowHint($"<size=16><align=Left>{hero}</align></size><size=12><align=Right>{StringBuilderPool.Shared.ToStringReturn(abilites)}</align></size>", short.MaxValue);
            }
        }
    }
}
