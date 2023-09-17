using Exiled.API.Features;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Features
{
    public static class Cooldown
    {
        private static Dictionary<string, CooldownInfo> cooldowns = new Dictionary<string, CooldownInfo>();
 
        public static CooldownInfo AddCooldown(string userId, CooldownInfo cooldownInfo)
        {
            cooldowns[userId] = cooldownInfo;
            return cooldowns[userId];
        }

        public static CooldownInfo GetCooldownOrDefault(string userId, string name)
        {
            return cooldowns.FirstOrDefault(cooldown => cooldown.Key == userId).Value;
        }
    }
}
