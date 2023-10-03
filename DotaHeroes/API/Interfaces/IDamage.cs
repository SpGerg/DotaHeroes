using DotaHeroes.API.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Interfaces
{
    /// <summary>
    /// Damage and damage type
    /// </summary>
    public interface IDamage
    {
        decimal Damage { get; set; }

        DamageType DamageType { get; set; }
    }
}
