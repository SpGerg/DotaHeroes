using DotaHeroes.API.Enums;
using DotaHeroes.API.Interfaces;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DotaHeroes.API.Features
{
    public static class Utils
    {
        public static ArraySegment<string> EmptyArraySegment { get; } = new ArraySegment<string>();

        public static List<int> EmptyLevelsList { get; } = new List<int>();

        public static List<int> DefaultLevelsUltimateList { get; } = new List<int>()
        {
            6,
            12,
            18
        };

        private static SideType[] sides = new SideType[] { SideType.Dire, SideType.Radiant };

        /// <summary>
        /// Get position from player eye direction with range
        /// </summary>
        public static Vector3 GetTargetPositionFromMouse(Vector3 position, Vector3 direction, int range)
        {
            Ray r = new(position, direction);

            return r.GetPoint(range);
        }

        /// <summary>
        /// Get position from player eye direction with range
        /// </summary>
        public static double GetPercentOfValue(double value, double percent)
        {
            return (value / 100) * percent;
        }

        /// <summary>
        /// Attribute type to statistics type
        /// </summary>
        public static StatisticsType ToStatisticsType(AttributeType attribute)
        {
            switch (attribute)
            {
                case AttributeType.Strength:
                    return StatisticsType.Strength;
                case AttributeType.Agility:
                    return StatisticsType.Agility;
                case AttributeType.Intelligence:
                    return StatisticsType.Intelligence;
                case AttributeType.Universal:
                    return StatisticsType.AllAttributes;
            }

            return StatisticsType.None;
        }

        /// <summary>
        /// Get player from eye direction
        /// </summary>
        public static bool GetPlayerFromEyeDirection(Player player, float range, out string response, out Player target)
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
        /// Get hero from eye direction with player
        /// </summary>
        public static bool GetHeroFromPlayerEyeDirection(Player player, float range, out string response, out Hero hero)
        {
            if (!GetPlayerFromEyeDirection(player, range, out response, out Player target))
            {
                hero = null;
                return false;
            }

            var _hero = DTAPI.GetHeroOrDefault(target.Id);

            if (_hero == default)
            {
                response = "Target is not hero2";
                hero = null;
                return false;
            }

            if (_hero.Player == player)
            {
                response = "Target is owner";
                hero = _hero;
                return false;
            }

            response = "Hero is " + player.Nickname;
            hero = _hero;
            return true;
        }

        /// <summary>
        /// Get hero from eye direction with hero
        /// </summary>
        public static bool GetHeroFromPlayerEyeDirection(Hero hero, float range, out string response, out Hero target, bool isIgnoreSide = false)
        {
            if (!GetHeroFromPlayerEyeDirection(hero.Player, range, out response, out Hero _target))
            {
                target = _target;
                return false;
            }

            if (_target.SideType == hero.SideType && !isIgnoreSide)
            {
                response = "Target is your teammate";
                target = _target;
                return false;
            }

            response = "Hero is " + hero.Player.Nickname;
            target = _target;
            return true;
        }

        /// <summary>
        /// Block damage
        /// </summary>
        public static double BlockDamage(double damage, DamageType damageType, IDamageBlock damageBlock)
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


        public static void AddModifier(Hero hero, IModifier modifier)
        {
            if (modifier is IAccuracyModifier accuracyModifier)
            {
                if (hero.HeroStatistics.Evasion.AccuracyModifier == null || hero.HeroStatistics.Evasion.AccuracyModifier.Accuracy < accuracyModifier.Accuracy)
                {
                    hero.HeroStatistics.Evasion.AccuracyModifier = accuracyModifier;
                }
            }

            if (modifier is IEvasionModifier evasionModifier)
            {
                hero.HeroStatistics.Evasion.EvasionModifiers.Add(evasionModifier);
            }

            if (modifier is IBlindModifier blindModifier)
            {
                hero.HeroStatistics.Evasion.BlindModifiers.Add(blindModifier);
            }

            if (modifier is IResistanceModifier resistanceModifier)
            {   
                hero.HeroStatistics.Resistance.ResistanceModifiers.Add(resistanceModifier);
            }

            if (modifier is ISpeedModifier speedModifier)
            {
                hero.HeroStatistics.Speed.Speed += speedModifier.Speed;
            }

            if (modifier is IArmorModifier armorModifier)
            {
                hero.HeroStatistics.Armor.ArmorModifiers.Add(armorModifier);
            }

            if (modifier is INegativeArmorModifier negativeArmorModifier)
            {
                hero.HeroStatistics.Armor.NegativeArmorModifiers.Add(negativeArmorModifier);
            }
        }

        public static void RemoveModifier(Hero hero, IModifier modifier)
        {
            if (modifier is IAccuracyModifier accuracyModifier)
            {
                hero.HeroStatistics.Evasion.AccuracyModifier = null;
            }

            if (modifier is IEvasionModifier evasionModifier)
            {
                hero.HeroStatistics.Evasion.EvasionModifiers.Remove(evasionModifier);
            }

            if (modifier is IBlindModifier blindModifier)
            {
                hero.HeroStatistics.Evasion.BlindModifiers.Remove(blindModifier);
            }

            if (modifier is IResistanceModifier resistanceModifier)
            {
                hero.HeroStatistics.Resistance.ResistanceModifiers.Remove(resistanceModifier);
            }

            if (modifier is ISpeedModifier speedModifier)
            {
                hero.HeroStatistics.Speed.Speed -= speedModifier.Speed;
            }

            if (modifier is IArmorModifier armorModifier)
            {
                hero.HeroStatistics.Armor.ArmorModifiers.Remove(armorModifier);
            }

            if (modifier is INegativeArmorModifier negativeArmorModifier)
            {
                hero.HeroStatistics.Armor.NegativeArmorModifiers.Remove(negativeArmorModifier);
            }
        }

        public static SideType GetRandomSide()
        {
            return sides[UnityEngine.Random.Range(0, 2)]; //interesting fact. Range(0, 1) is always 0. Lol
        }
    }
}
