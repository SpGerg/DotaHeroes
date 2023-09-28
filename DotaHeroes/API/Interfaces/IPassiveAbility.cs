using DotaHeroes.API.Features;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Interfaces
{
    public interface IPassiveAbility
    {
        void Register(Hero owner);

        void Unregister(Hero owner);
    }
}
