using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Features
{
    public abstract class PassiveAbility : Ability
    {
        public bool IsEnabled { get; set; } = true;

        public PassiveAbility() { }

        public PassiveAbility(Player owner)
        {
            Owner = owner;
        }
    }
}
