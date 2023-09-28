using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Enums
{
    ///<summary>Target type</summary>
    public enum TargetType
    {
        None = 0,
        ToPoint = 1,
        ToEnemy = 2,
        ToFriend = 3,
        ToFriendAndEnemy = 4,
        ToPointOrEnemy = 5,
        ToPointOrFriend = 6,
        ToPointOrFriendAndEnemy = 6,
    }
}
