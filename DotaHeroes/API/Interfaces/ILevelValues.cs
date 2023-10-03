﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Interfaces
{
    /// <summary>
    /// Level values, damage, cooldown.
    /// 
    /// If you set add cooldown to dictionary and call base constructor, it is automatically add cooldowns (on level up too).
    /// </summary>
    public interface ILevelValues : ILevelUp
    {
        Dictionary<string, List<float>> Values { get; }
    }
}