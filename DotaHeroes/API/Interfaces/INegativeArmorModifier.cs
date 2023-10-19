using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Interfaces
{
    public interface INegativeArmorModifier : IModifier
    {
        decimal NegativeArmor { get; set; }
    }
}
