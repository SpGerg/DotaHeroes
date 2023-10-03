using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Interfaces
{
    /// <summary>
    /// Level up info, max level, min level.
    /// 
    /// If HeroLevelToLevelUp is empty - level upping every level.
    /// </summary>
    public interface ILevelUp
    {
        int MaxLevel { get; set; }

        int MinLevel { get; set; }

        IReadOnlyList<int> HeroLevelToLevelUp { get; }
    }
}
