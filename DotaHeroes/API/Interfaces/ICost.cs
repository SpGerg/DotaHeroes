﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Interfaces
{
    public interface ICost
    {
        int ManaCost { get; set; }

        int HealthCost { get; set; }
    }
}
