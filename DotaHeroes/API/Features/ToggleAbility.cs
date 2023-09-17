using CommandSystem;
using DotaHeroes.API.Features;
using DotaHeroes.API.Interfaces;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEngine.UI.GridLayoutGroup;

namespace DotaHeroes.API.Features
{
    public abstract class ToggleAbility : Ability, ICommand
    {
        public virtual string Command { get; }

        public virtual string[] Aliases { get; } = Array.Empty<string>();

        public virtual string Desciption { get; }

        public abstract bool IsActive { get; set; }

        public ToggleAbility() : base()
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

            IsActive = !IsActive;

            return Execute(hero, arguments, out response);
        }
    }
}
