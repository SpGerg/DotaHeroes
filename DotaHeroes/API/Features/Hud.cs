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
            if (hero == null) return;
            if (hero.IsHeroDead) return;

            var player = hero.Player;
            var abilites = StringBuilderPool.Shared.Rent();

            foreach (var ability in hero.Abilities)
            {
                abilites.AppendLine(ability.Name);

                if (ability is ActiveAbility or PassiveAbility)
                {
                    abilites.AppendLine("Cooldown: " + Cooldowns.ToStringIsCooldown(hero.Player.Id, ability.Slug));
                }
                else if (ability is ToggleAbility passiveAbility)
                {
                    abilites.AppendLine("- " + passiveAbility.ToStringIsActive());
                }
            }

            var index = 0;

            foreach (var item in hero.Inventory.GetItems())
            {
                abilites.AppendLine($"{index}: {item.Name}");

                if (item.MainAbility is ActiveAbility or PassiveAbility)
                {
                    abilites.AppendLine("Cooldown: " + Cooldowns.ToStringIsCooldown(hero.Player.Id, item.Slug));
                }
                else if (item.MainAbility is ToggleAbility passiveAbility)
                {
                    abilites.AppendLine("- " + passiveAbility.ToStringIsActive());
                }

                index++;
            }

            player.ShowHint($"<pos=0%><size=16><align=Left>{hero}</align></size><size=12><align=Right>{StringBuilderPool.Shared.ToStringReturn(abilites)}</align></size>", short.MaxValue);
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
