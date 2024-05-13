using CommandSystem;
using Exiled.API.Features;
using RemoteAdmin;
using System;

namespace DotaHeroes.Commands
{
    public abstract class AdminCommandBase : ICommand
    {
        public abstract string Command { get; }

        public abstract string Description { get; }

        public virtual string[] Aliases => new string[0];

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender is not PlayerCommandSender)
            {
                response = "You must be player";
                return false;
            }

            var player = Player.Get(sender);

            if (!player.RemoteAdminAccess && !player.RemoteAdminPermissions.HasFlag(PlayerPermissions.ServerConsoleCommands))
            {
                response = "Your havent access";
                return false;
            }

            return Execute(player, arguments, out response);
        }

        public abstract bool Execute(Player player, ArraySegment<string> arguments, out string response);
    }
}
