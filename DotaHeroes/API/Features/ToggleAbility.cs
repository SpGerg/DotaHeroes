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
    public abstract class ToggleAbility : Ability, ICommand
    {
        public abstract bool IsActive { get; set; }

        public abstract string Command { get; }

        public abstract string[] Aliases { get; }

        public ToggleAbility() { }

        public ToggleAbility(Player owner)
        {
            Owner = owner;
        }

        public abstract bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response);
    }
}
