using System;

namespace DotaHeroes.API.Enums
{
    ///<summary>Target type</summary>
    [Flags]
    public enum TargetType
    {
        None = 0,
        ToPoint = 1,
        ToEnemy = 2,
        ToFriend = 4,
    }
}
