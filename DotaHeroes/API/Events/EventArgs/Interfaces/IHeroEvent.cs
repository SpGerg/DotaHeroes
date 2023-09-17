using DotaHeroes.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Events.EventArgs.Interfaces
{
    public interface IHeroEvent
    {
        Features.Hero Hero { get; set; }
    }
}
