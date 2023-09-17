using CommandSystem;
using DotaHeroes.API.Features;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Features
{
    [CommandHandler(typeof(ClientCommandHandler))]
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public abstract class ActiveAbility : Ability, ICommand
    {
        public string Command { get; set; }

        public virtual string[] Aliases { get; set; } = Array.Empty<string>();

        public ActiveAbility() { }

        public ActiveAbility(Player owner)
        { 
            Owner = owner;

            Command = Name;
        }

        public abstract bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response);
    }
}
