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
        public static void RunUpdate()
        {
            Timing.RunCoroutine(UpdateCoroutine());
        }

        private static IEnumerator<float> UpdateCoroutine()
        {
            while (true)
            {
                foreach (var hero in API.GetHeroes().Values)
                {
                    if (hero.IsHeroDead) continue;

                    var player = hero.Player;
                    var cooldowns = Cooldowns.GetCooldownInfo(player.UserId);

                    if (string.IsNullOrEmpty(cooldowns))
                    {
                        player.ShowHint($"<size=16><align=Left>{hero}</align></size>", 1.5f);
                    }
                    else
                    {
                        player.ShowHint($"<size=16><align=Left>{hero}</align></size><size=16><align=Right>{cooldowns}</align></size>", 1.5f);
                    }
                }

                yield return Timing.WaitForSeconds(1);
            }
        }
    }
}
