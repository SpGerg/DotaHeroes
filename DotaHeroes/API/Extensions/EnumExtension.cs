using DotaHeroes.API.Enums;
using Exiled.API.Features;
using NorthwoodLib.Pools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Extensions
{
    public static class EnumExtension
    {
        /*
        public static string ToStringWithSpaces(this Enum _enum)
        {
            StringBuilder stringBuilder = StringBuilderPool.Shared.Rent();

            string line = string.Empty;

            foreach (char letter in _enum.ToString())
            {
                if (letter == _enum.ToString()[0])
                {
                    line += letter;

                    continue;
                }

                if (char.IsUpper(letter))
                {
                    stringBuilder.Append(line + " ");

                    line = string.Empty;

                    continue;
                }
            }

            stringBuilder.Append(line + " ");

            return StringBuilderPool.Shared.ToStringReturn(stringBuilder);
        }
        */

        public static string ToStringWithColor(this DamageType damageType)
        {
            switch (damageType)
            {
                case DamageType.None:
                    return damageType.ToString();
                case DamageType.Physical:
                    return $"<color=#b21515>{damageType}</color>";
                case DamageType.Magical:
                    return $"<color=#64b7d3>{damageType}</color>";
                case DamageType.Pure:
                    return $"<color=#ffef14>{damageType}</color>";
            }

            return damageType.ToString();
        }
    }
}
