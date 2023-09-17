using CommandSystem;
using DotaHeroes.API.Features;
using DotaHeroes.API.Interfaces;
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
    public abstract class ToggleAbility : Ability, ICommand
    {
        public string Command { get; }

        public virtual string[] Aliases { get; } = Array.Empty<string>();

        public abstract bool IsActive { get; set; }

        public ToggleAbility() { }

        public ToggleAbility(Player owner)
        {
            Owner = owner;

            Command = Name;
        }

        public abstract bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response);
    }
}
