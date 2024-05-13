using CommandSystem;
using Mono.Collections.Generic;
using NorthwoodLib.Pools;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHeroes.Commands.User.Hero
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Parent : ParentCommand
    {
        public override string Command => "hero";

        public override string[] Aliases => new string[0];

        public override string Description => "Hero commands";

        public override IEnumerable<ICommand> AllCommands => new Collection<ICommand>()
        {
            new Attack(),
            new UseAbility(),
            new BuyItem(),
            new SellItem(),
            new Inventory(),
            new LevelUp()
        };

        private string message = string.Empty;

        public Parent()
        {
            LoadGeneratedCommands();
        }

        public override void LoadGeneratedCommands()
        {
            StringBuilder stringBuilder = StringBuilderPool.Shared.Rent();

            foreach (var command in AllCommands)
            {
                stringBuilder.AppendLine(command.Command + ": " + command.Description);
                RegisterCommand(command);
            }

            message = StringBuilderPool.Shared.ToStringReturn(stringBuilder);
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = message;
            return true;
        }
    }
}
