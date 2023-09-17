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
    public abstract class ActiveAbility : Ability, ICommand
    {
        public virtual string Command { get; set; }

        public virtual string[] Aliases { get; set; } = Array.Empty<string>();

        public ActiveAbility() : base()
        {
            if (string.IsNullOrEmpty(Command))
            {
                Command = Name;
            }
        }

        public abstract bool Execute(Hero hero, ArraySegment<string> arguments, out string response);

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!base.Execute(sender, out response, out Hero hero, true))
            {
                return false;
            }

            return Execute(hero, arguments, out response);
        }
    }
}
