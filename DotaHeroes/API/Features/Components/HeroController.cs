﻿using DotaHeroes.API.Enums;
using Exiled.API.Features;
using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DotaHeroes.API.Features.Components
{
    public class HeroController : NetworkBehaviour
    {
        public Hero Hero { get; set; }
    }
}
