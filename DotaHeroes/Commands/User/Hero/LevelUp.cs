using NorthwoodLib.Pools;
using System;

namespace DotaHeroes.Commands.User.Hero
{
    public class LevelUp : HeroCommandBase
    {
        public override string Command => "level_up";

        public override string Description => "Level up ability";

        protected override bool Execute(API.Features.Hero hero, ArraySegment<string> arguments, out string response)
        {
            if (arguments.Count == 0)
            {
                var stringBuilder = StringBuilderPool.Shared.Rent();

                stringBuilder.AppendLine($"Command format: .levelup <index>");
                stringBuilder.AppendLine($"Command example: .levelup 0");

                var index = 0;

                foreach (var item in hero.Abilities)
                {
                    stringBuilder.AppendLine($"{index}: {item.Name}");
                    stringBuilder.AppendLine($"- Level: {item.Level + 1}");
                    index++;
                }

                response = StringBuilderPool.Shared.ToStringReturn(stringBuilder);
                return false;
            }

            if (!int.TryParse(arguments.Array[2], out int result))
            {
                response = "Second argument is not number";
                return false;
            }

            if (result < 0 | result > hero.Abilities.Count)
            {
                response = "Im think something broken in your second argument.";
                return true;
            }

            var _item = hero.Abilities[result];

            if (hero.LevelUpAbilty(_item.Slug))
            {
                response = $"Ability {_item.Name} was level upped";
            }
            else
            {
                response = $"Ability {_item.Name} was not level upped, you havent points to level up or you cant level up this.";
            }

            return true;
        }
    }
}
