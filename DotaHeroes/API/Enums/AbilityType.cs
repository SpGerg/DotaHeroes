using System;

namespace DotaHeroes.API.Enums
{
    ///<summary>Ability type</summary>
    [Flags]
    public enum AbilityType
    {
        None = 1,
        Active = 2,
        Passive = 4,
        Toggle = 8
    }
}
