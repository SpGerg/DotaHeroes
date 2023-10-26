using CommandSystem;
using Exiled.API.Features;
using DotaHeroes.API.Features;
using RemoteAdmin;
using System;

namespace DotaHeroes.Commands
{
    public abstract class HeroCommandBase : ICommand
    {
        public abstract string Command { get; }

        public abstract string Description { get; }

        public virtual string[] Aliases { get; } = new string[0];

        /// <summary>
        /// Execute
        /// </summary>
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender is not PlayerCommandSender)
            {
                response = "Your are not player.";

                return false;
            }

            var player = Player.Get(sender);
            Hero hero = API.DTAPI.GetHeroOrDefault(player.Id);

            if (hero == default)
            {
                response = "Your are not hero.";

                return false;
            }

            return Execute(hero, arguments, out response);
        }

        /// <summary>
        /// Abstract execute. ICommandSender to Hero.
        /// </summary>
        protected abstract bool Execute(Hero hero, ArraySegment<string> arguments, out string response);
    }
}
