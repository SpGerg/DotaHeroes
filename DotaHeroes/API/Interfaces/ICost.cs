using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Interfaces
{
    /// <summary>
    /// Ability cost, mana and health.
    /// </summary>
    public interface ICost
    {
        int ManaCost { get; set; }

        int HealthCost { get; set; }
    }
}
