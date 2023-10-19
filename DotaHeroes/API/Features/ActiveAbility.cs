using CommandSystem;
using DotaHeroes.API.Events.Handlers;
using DotaHeroes.API.Features;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Features
{
    public abstract class ActiveAbility : Ability
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActiveAbility" /> class.
        /// </summary>
        public ActiveAbility() : base() { }

        protected abstract bool Execute(Hero hero, ArraySegment<string> arguments, out string response);

        /// <summary>
        /// Execute.
        /// </summary>
        public virtual bool CheckAndExecute(Hero hero, ArraySegment<string> arguments, out string response)
        {
            if (!base.Execute(hero, out response, true))
            {
                return false;
            }

            if (!CheckCooldown(hero, out response, out CooldownInfo cooldown))
            {
                return false;
            }

            if (!Execute(hero, arguments, out response))
            {
                return false;
            }

            RunCooldown(hero, cooldown);
            return true;
        }
    }
}
