using Exiled.API.Features;
using MEC;
using NorthwoodLib.Pools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Features
{
    public static class Cooldowns
    {
        private static Dictionary<int, Dictionary<string, CooldownInfo>> cooldowns = new Dictionary<int, Dictionary<string, CooldownInfo>>();

        /// <summary>
        /// Add ability cooldown for player.
        /// </summary>
        public static CooldownInfo AddCooldown(int id, CooldownInfo cooldownInfo)
        {
            try
            {
                cooldowns[id][cooldownInfo.Name] = cooldownInfo;
            }
            catch (KeyNotFoundException)
            {
                cooldowns[id] = new Dictionary<string, CooldownInfo>();
                cooldowns[id][cooldownInfo.Name] = cooldownInfo;
            }
            return cooldowns[id][cooldownInfo.Name];
        }

        /// <summary>
        /// Get ability cooldown from player, if not exists create new cooldown info.
        /// </summary>
        public static CooldownInfo GetCooldown(int id, string name)
        {
            try
            {
                return cooldowns[id][name];
            }
            catch (KeyNotFoundException)
            {
                cooldowns[id] = new Dictionary<string, CooldownInfo>();
                cooldowns[id][name] = new CooldownInfo(name, 3);
                return cooldowns[id][name];
            }
        }

        /// <summary>
        /// To string is cooldown
        /// </summary>
        public static string ToStringIsCooldown(int id, string name)
        {
            if (!cooldowns.ContainsKey(id))
            {
                cooldowns[id] = new Dictionary<string, CooldownInfo>();
                return string.Empty;
            }

            if (cooldowns[id].Count == 0)
            {
                return string.Empty;
            }

            if (!cooldowns[id].ContainsKey(name))
            {
                return string.Empty;
            }

            var cooldown = cooldowns[id][name];

            if (cooldown.IsReady)
            {
                return $"{cooldown.Name}: <color=Green>Ready</color>";
            }

            return $"{cooldown.Name}: {cooldown.Cooldown}";
        }
    }
}
