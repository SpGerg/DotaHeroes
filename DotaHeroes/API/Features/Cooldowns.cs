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
        private static Dictionary<string, Dictionary<string, CooldownInfo>> cooldowns = new Dictionary<string, Dictionary<string, CooldownInfo>>();
 
        public static CooldownInfo AddCooldown(string userId, CooldownInfo cooldownInfo)
        {
            try
            {
                cooldowns[userId][cooldownInfo.Name] = cooldownInfo;
            }
            catch (KeyNotFoundException)
            {
                cooldowns[userId] = new Dictionary<string, CooldownInfo>();
                cooldowns[userId][cooldownInfo.Name] = cooldownInfo;
            }
            return cooldowns[userId][cooldownInfo.Name];
        }

        public static CooldownInfo GetCooldown(string userId, string name)
        {
            try
            {
                return cooldowns[userId][name];
            }
            catch (KeyNotFoundException)
            {
                cooldowns[userId] = new Dictionary<string, CooldownInfo>();
                cooldowns[userId][name] = new CooldownInfo(name, 3);
                return cooldowns[userId][name];
            }
        }

        public static string ToStringIsCooldown(string userId, string name)
        {
            if (!cooldowns.ContainsKey(userId))
            {
                cooldowns[userId] = new Dictionary<string, CooldownInfo>();
                return string.Empty;
            }

            if (cooldowns[userId].Count == 0)
            {
                return string.Empty;
            }

            if (!cooldowns[userId].ContainsKey(name))
            {
                return string.Empty;
            }

            var cooldown = cooldowns[userId][name];

            if (cooldown.IsReady)
            {
                return $"{cooldown.Name}: <color=Green>Ready</color>";
            }

            return $"{cooldown.Name}: {cooldown.Cooldown}";
        }
    }
}
