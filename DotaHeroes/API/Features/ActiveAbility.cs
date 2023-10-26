using Exiled.API.Features;
using System;

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
            if (Plugin.Instance.Config.Debug) Log.Info("to execute");

            if (!base.Execute(hero, out response, true))
            {
                return false;
            }

            if (Plugin.Instance.Config.Debug) Log.Info("after execute");

            if (!CheckCooldown(hero, out response, out CooldownInfo cooldown))
            {
                return false;
            }

            if (Plugin.Instance.Config.Debug) Log.Info("after cooldown");

            if (!Execute(hero, arguments, out response))
            {
                return false;
            }

            if (Plugin.Instance.Config.Debug) Log.Info("after abstract execute");

            RunCooldown(hero, cooldown);
            return true;
        }
    }
}
