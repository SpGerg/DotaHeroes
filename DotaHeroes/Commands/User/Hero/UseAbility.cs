using CommandSystem;
using DotaHeroes.API.Features;
using Exiled.API.Features;
using NorthwoodLib.Pools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.Commands.User.Hero
{
    [CommandHandler(typeof(ClientCommandHandler))]
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class UseAbility : HeroCommandBase
    {
        public override string Command => "use_ability";

        public override string Description => "Using ability";

        protected override bool Execute(API.Features.Hero hero, ArraySegment<string> arguments, out string response)
        {
            if (arguments.Count == 0)
            {
                StringBuilder stringBuilder = StringBuilderPool.Shared.Rent();

                stringBuilder.AppendLine("Command format: use_ability <ability index> <is item>");
                stringBuilder.AppendLine("Command example: use_ability 1 false. It will be use first ability (Meat hook for example).");
                stringBuilder.AppendLine("Another command example: use_ability 0 true. It will be use first item (Armlet of Mordiggian for example).");
                stringBuilder.AppendLine("Abilities list and index: ");

                int index = 0;

                foreach (var ability in hero.Abilities)
                {
                    stringBuilder.AppendLine($"{index}: {ability.Name}");
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

            if (!bool.TryParse(arguments.Array[3], out bool _result))
            {
                response = "Third argument is not bool value, bro.";

                return false;
            }

            response = "Item was used";

            if (result < 0)
            {
                response = $"{result} ability, yes?.. ";
                return false;
            }

            if (_result)
            {
                if (result > hero.Inventory.GetItems().Count)
                {
                    response = $"{result} ability, yes?.. ";
                    return false;
                }

                return hero.Inventory.ExecuteItem(result);
            }
            else
            {
                if (result > hero.Abilities.Count)
                {
                    response = $"{result} ability, yes?.. ";
                    return false;
                }

                return hero.ExecuteAbility(hero.Abilities[result], ref response);
            }
        }
    }
}
