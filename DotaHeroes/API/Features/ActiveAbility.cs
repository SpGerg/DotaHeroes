using CommandSystem;
using DotaHeroes.API.Features;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Interfaces
{
    public abstract class ActiveAbility : Ability, ICommand
    {
        public abstract string Command { get; }

        public abstract string[] Aliases { get; }

        public ActiveAbility() { }

        public ActiveAbility(Player owner)
        { 
            Owner = owner;
        }
    }
}
