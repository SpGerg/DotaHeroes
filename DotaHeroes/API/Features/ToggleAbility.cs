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

        /// <summary>
        /// Initializes a new instance of the <see cref="ToggleAbility" /> class.
        /// </summary>
        public ToggleAbility() : base()
        {
            if (string.IsNullOrEmpty(Command))
            {
                Command = Name.Replace(" ", "").ToLower();
            }
        }

        /// <summary>
        /// Activate
        /// </summary>
        public abstract bool Activate(Hero hero, ArraySegment<string> arguments, out string response);

        /// <summary>
        /// Deactivate
        /// </summary>
        public abstract bool Deactivate(Hero hero, ArraySegment<string> arguments, out string response);

        /// <summary>
        /// Base ability execute
        /// </summary>
        public bool Execute(Hero hero, ArraySegment<string> arguments, out string response)
        {
            IsActive = !IsActive;

            if (IsActive)
            {
                return Activate(hero, arguments, out response);
            }
            else
            {
                return Deactivate(hero, arguments, out response);
            }
        }

        /// <summary>
        /// Execute
        /// </summary>
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!base.Execute(sender, out response, out Hero hero, true))
            {
                return false;
            }

            IsActive = !IsActive;

            CheckAndRunCooldown(hero, out string _);

            if (IsActive)
            {
                return Activate(hero, arguments, out response);
            }
            else
            {
                return Deactivate(hero, arguments, out response);
            }
        }
    }
}
