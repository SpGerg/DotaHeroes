﻿using System.Collections.Generic;

namespace DotaHeroes.API.Interfaces
{
    /// <summary>
    /// Level values, damage, cooldown.
    /// 
    /// If you set add cooldown, damage, mana cost and health cost to dictionary and call base constructor, it is automatically be set (on level up too).
    /// </summary>
    public interface ILevelValues : ILevelUp
    {
        Dictionary<string, List<double>> Values { get; }
    }
}
