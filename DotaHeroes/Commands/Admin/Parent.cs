using CommandSystem;
using Exiled.API.Features.Pools;
using Mono.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Text;

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
            StringBuilder stringBuilder = StringBuilderPool.Pool.Get();

            foreach (var command in AllCommands)
            {
                stringBuilder.AppendLine(command.Command + ": " + command.Description);
                RegisterCommand(command);
            }

            message = StringBuilderPool.Pool.ToStringReturn(stringBuilder);
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = message;
            return true;
        }
    }
}
