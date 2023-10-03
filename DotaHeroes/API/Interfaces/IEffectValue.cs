using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Interfaces
{
    /// <summary>
    /// Some effect value (stack)
    /// </summary>
    public interface IEffectValue
    {
        int Value { get; set; }
    }
}
