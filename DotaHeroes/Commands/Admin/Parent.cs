using CommandSystem;
using Mono.Collections.Generic;
using NorthwoodLib.Pools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.Commands.Admin
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Parent : ParentCommand
    {
        public override string Command => "hero_admin";

        public override string[] Aliases => new string[0];

        public override string Description => "Admin commands";

        public override IEnumerable<ICommand> AllCommands => new Collection<ICommand>()
        {
            new Info(),
            new SetHero(),
            new Statistics(),
            new HeroDummy()
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
