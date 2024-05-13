using DotaHeroes.API.Enums;
using System.Collections.Generic;

namespace DotaHeroes.API.Interfaces
{
    /// <summary>
    /// Damage block
    /// 
    /// DamagesTypesToBlock - None - all of damage types
    /// </summary>
    public interface IDamageBlock
    {
        int DamageBlock { get; set; }

        IReadOnlyList<DamageType> DamageTypesToBlock { get; }
    }
}
