using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Interfaces
{
    public abstract class PassiveAbility : Ability
    {
        public PassiveAbility() { }

        public PassiveAbility(Player owner)
        {
            Owner = owner;
        }
    }
}
