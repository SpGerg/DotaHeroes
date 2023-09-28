using DotaHeroes.API.Enums;
using DotaHeroes.API.Interfaces;
using Exiled.API.Features;
using NorthwoodLib.Pools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DotaHeroes.API.Features
{
    public static class Utils
    {
        /// <summary>
        /// Get position from player eye direction with range
        /// </summary>
        public static Vector3 GetTargetPositionFromMouse(Vector3 position, Vector3 direction, int range)
        {
            Ray r = new(position, direction);

            return r.GetPoint(range);
        }

        /// <summary>
        /// Block damage
        /// </summary>
        public static int BlockDamage(int damage, DamageType damageType, IDamageBlock damageBlock)
        {
            if (damageBlock.DamageTypesToBlock.Contains(DamageType.None))
            {
                return damage - damageBlock.DamageBlock;
            }

            if (damageBlock.DamageTypesToBlock.Contains(damageType))
            {
                return damage - damageBlock.DamageBlock;
            }

            return damage;
        }
    }
}
