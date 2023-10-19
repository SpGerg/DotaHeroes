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
    public abstract class ToggleAbility : Ability
    {
        public virtual string Desciption { get; }

        public bool IsActive { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ToggleAbility" /> class.
        /// </summary>
        public ToggleAbility() : base() { }

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
        public virtual bool CheckAndExecute(Hero hero, ArraySegment<string> arguments, out string response)
        {
            if (!base.Execute(hero, out response, true))
            {
                return false;
            }

            //if (!CheckCooldown(hero, out response, out CooldownInfo cooldown))
            //{
                //return false;
            //}

            IsActive = !IsActive;

            if (IsActive)
            {
                if (!Activate(hero, arguments, out response))
                {
                    return false;
                }
            }
            else
            {
                if (!Deactivate(hero, arguments, out response))
                {
                    return false;
                }
            }

            RunCooldown(hero, default);
            return true;
        }

        public string ToStringIsActive()
        {
            return IsActive == true ? "Enabled" : "Disabled";
        }
    }
}
