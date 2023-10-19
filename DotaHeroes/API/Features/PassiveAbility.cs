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
        public Hero Owner { get; private set; }

        public void RegisterOwner(Hero owner)
        {
            Owner = owner;
        }

        public abstract void Register(Hero owner);

        public abstract void Unregister(Hero owner);
    }
}
