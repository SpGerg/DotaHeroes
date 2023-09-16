using CommandSystem;
using DotaHeroes.API.Features.Components;
using Exiled.API.Features;
using DotaHeroes.API.Features;
using RemoteAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.Commands
{
    public abstract class HeroCommandBase : ICommand
    {
        public abstract string Command { get; }

        public abstract string[] Aliases { get; }

        public abstract string Description { get; }

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender is not PlayerCommandSender)
            {
                response = "Your are not player.";

                return false;
            }

            var player = Player.Get(sender);
            API.Features.Hero hero = API.API.GetHeroOrDefault(player.UserId);

            if (hero == default)
            {
                response = "Your are not hero.";

                return false;
            }

            return Execute(hero, arguments, out response);
        }

        protected abstract bool Execute(API.Features.Hero hero, ArraySegment<string> arguments, out string response);
    }
}
