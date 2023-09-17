using DotaHeroes.API.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Interfaces
{
    public interface IDamageBlock
    {
        int DamageBlock { get; set; }

        IReadOnlyList<DamageType> DamageTypesToBlock { get; } //None - all of damage types
    }
}
