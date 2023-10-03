using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Interfaces
{
    /// <summary>
    /// Effect duration
    /// </summary>
    public interface IEffectDuration
    {
        float? Duration { get; }
    }
}
