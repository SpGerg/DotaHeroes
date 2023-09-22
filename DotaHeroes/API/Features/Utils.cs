using DotaHeroes.API.Enums;
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
        public static Vector3 GetTargetPositionFromMouse(Vector3 position, Vector3 direction, int range)
        {
            Ray r = new(position, direction);

            return r.GetPoint(range);
        }

        public static int BlockDamage(int damage, DamageType damageType, int blockDamage, IReadOnlyList<DamageType> blockDamageTypes)
        {
            if (blockDamageTypes.Contains(DamageType.None))
            {
                return damage - blockDamage;
            }

            if (blockDamageTypes.Contains(damageType))
            {
                return damage - blockDamage;
            }

            return damage;
        }
    }
}
