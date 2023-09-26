using DotaHeroes.API.Enums;
using DotaHeroes.API.Events.EventArgs.Interfaces;
using DotaHeroes.API.Interfaces;
using Exiled.Events.EventArgs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Events.EventArgs.Hero
{
    public class HeroTakingDamageEventArgs : IHeroEvent, IDamage, IDeniableEvent
    {
        public Features.Hero Hero { get; }

        public Features.Hero Attacker { get; }

        public int Damage { get; set; }

        public DamageType DamageType { get; set; }

        public bool IsAllowed { get; set; }

        public HeroTakingDamageEventArgs(Features.Hero hero, Features.Hero attacker, int damage, DamageType damageType, bool isAllowed)
        {
            Hero = hero;
            Attacker = attacker;
            Damage = damage;
            DamageType = damageType;
            IsAllowed = isAllowed;
        }
    }
}
