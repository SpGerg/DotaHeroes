using Exiled.API.Features;
using NorthwoodLib.Pools;

namespace DotaHeroes.API.Features
{
    public static class Hud
    {
        /// <summary>
        /// Update hud to all heroes.
        /// </summary>
        public static void Update()
        {
            foreach (var hero in DTAPI.GetHeroes().Values)
            {
                Update(hero);
            }
        }

        /// <summary>
        /// Update hud to hero.
        /// </summary>
        public static void Update(Hero hero)
        {
            if (hero == null || hero.Player == null || hero.IsHeroDead) return;

            var player = hero.Player;
            var abilites = StringBuilderPool.Shared.Rent();

            foreach (var ability in hero.Abilities)
            {
                abilites.AppendLine(ability.ToStringHud(hero));
            }

            var index = 0;

            foreach (var item in hero.Inventory.GetItems())
            {
                abilites.AppendLine($"{index}: {item.ToStringHud(hero)}");

                index++;
            }

            player.ShowHint($"<pos=0><size=16><align=Left>{hero}</align></size></pos><size=12><align=Right>{StringBuilderPool.Shared.ToStringReturn(abilites)}</align></size>", short.MaxValue);
        }

        /// <summary>
        /// Clear hud to hero.
        /// </summary>
        public static void Clear(Player player)
        {
            player.ShowHint(string.Empty, 0);
        }
    }
}
