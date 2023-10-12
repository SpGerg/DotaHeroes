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
        /// Get player from eye direction
        /// </summary>
        public static bool GetPlayerFromEyeDirection(Player player, int range, out string response, out Player target)
        {
            RaycastHit hit;

            if (!Physics.Raycast(player.CameraTransform.position, player.CameraTransform.forward, out hit, range))
            {
                response = "Target not found";
                target = null;
                return false;
            }

            var _target = Player.Get(hit.collider);

            if (_target == null)
            {
                response = "Target is not player";
                target = null;
                return false;
            }

            response = "Target is " + player.Nickname;
            target = _target;
            return true;
        }

        /// <summary>
        /// Get hero from eye direction
        /// </summary>
        public static bool GetHeroFromPlayerEyeDirection(Player player, int range, out string response, out Hero hero)
        {
            if (!GetPlayerFromEyeDirection(player, range, out response, out Player target))
            {
                hero = null;
                return false;
            }

            var _hero = API.GetHeroOrDefault(target.Id);

            if (_hero == default)
            {
                response = "Target is not hero";
                hero = null;
                return false;
            }

            if (_hero.Player == player)
            {
                response = "Target is owner";
                hero = null;
                return false;
            }

            response = "Hero is " + player.Nickname;
            hero = _hero;
            return true;
        }

        /// <summary>
        /// Block damage
        /// </summary>
        public static decimal BlockDamage(decimal damage, DamageType damageType, IDamageBlock damageBlock)
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
