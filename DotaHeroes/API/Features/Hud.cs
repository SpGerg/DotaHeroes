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
        public static Dictionary<string, List<string>> Players = new Dictionary<string, List<string>>();

        public static void RunUpdate()
        {
            Timing.RunCoroutine(UpdateCoroutine());
        }

        private static IEnumerator<float> UpdateCoroutine()
        {
            while (true)
            {
                foreach (var player in Player.List)
                {
                    var hero = API.GetHeroOrDefault(player.UserId);

                    if (hero == default)
                    {
                        continue;
                    }

                    player.Broadcast(4, $"<size=16><align=Left>{CreateHeroInfo(hero)}</align></size>");
                }

                yield return Timing.WaitForSeconds(3);
            }
        }

        public static string CreateHeroInfo(Hero hero)
        {
            var stringBuilder = StringBuilderPool.Shared.Rent();
            var effects = hero.GetEffects();

            stringBuilder.AppendLine("Hero: ");
            stringBuilder.AppendLine("Name: " + hero.HeroName);
            stringBuilder.AppendLine("Hero statistics: " + hero.HeroStatistics.ToString());

            stringBuilder.AppendLine(new string('—', 16));

            stringBuilder.AppendLine("Effects: ");

            foreach (var effect in effects)
            {
                if (effect.IsVisible)
                {
                    stringBuilder.AppendLine(effect.ToString());

                    stringBuilder.AppendLine($"— {effect.Description}");
                }
            }

            return StringBuilderPool.Shared.ToStringReturn(stringBuilder);
        }
    }
}
