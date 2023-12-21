using Exiled.API.Features;
using System;

namespace DotaHeroes.API.Features
{
    public abstract class ToggleAbility : Ability
    {
        public bool IsActive { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ToggleAbility" /> class.
        /// </summary>
        /// <param name="hero"><inheritdoc cref="Hero" /></param>
        public ToggleAbility(Hero hero) : base(hero)
        {
        }

        /// <summary>
        /// Activate
        /// </summary>
        public abstract bool Activate(ArraySegment<string> arguments, out string response);

        /// <summary>
        /// Deactivate
        /// </summary>
        public abstract bool Deactivate(ArraySegment<string> arguments, out string response);

        /// <summary>
        /// Base ability execute
        /// </summary>
        public virtual bool CheckAndExecute(ArraySegment<string> arguments, out string response)
        {
            if (!base.Execute(out response, true))
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
                if (!Activate(arguments, out response))
                {
                    return false;
                }
            }
            else
            {
                if (!Deactivate(arguments, out response))
                {
                    return false;
                }
            }

            RunCooldown(default);
            return true;
        }

        public string ToStringIsActive()
        {
            return IsActive == true ? "Enabled" : "Disabled";
        }
    }
}
