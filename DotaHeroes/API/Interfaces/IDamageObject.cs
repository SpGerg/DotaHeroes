using DotaHeroes.API.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Interfaces
{
    public interface IDamageObject
    {
        int Damage { get; set; }
        DamageType DamageType { get; set; }
    }
}
