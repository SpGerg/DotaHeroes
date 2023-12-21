using Exiled.API.Features;
using System;

namespace DotaHeroes.API.Features
{
    public abstract class ActiveAbility : Ability
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActiveAbility" /> class.
        /// </summary>
        public ActiveAbility(Hero hero) : base(hero) { }

        protected abstract bool Execute(ArraySegment<string> arguments, out string response);

        /// <summary>
        /// Execute.
        /// </summary>
        public virtual bool CheckAndExecute(ArraySegment<string> arguments, out string response)
        {
            Log.Debug("to execute");

            if (!base.Execute(out response, true))
            {
                return false;
            }

            Log.Debug("after execute");

            if (!CheckCooldown(out response, out CooldownInfo cooldown))
            {
                return false;
            }

            Log.Debug("after cooldown");

            if (!Execute(arguments, out response))
            {
                return false;
            }

            Log.Debug("after abstract execute");

            RunCooldown(cooldown);
            return true;
        }
    }
}
