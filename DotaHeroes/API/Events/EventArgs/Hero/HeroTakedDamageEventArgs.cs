using DotaHeroes.API.Enums;
using DotaHeroes.API.Events.EventArgs.Interfaces;
using DotaHeroes.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Events.EventArgs.Hero
{
    public class HeroTakedDamageEventArgs : IHeroEvent, IDamage
    {
        public Features.Hero Hero { get; set; }

        public Features.Hero Attacker { get; set; }

        public int Damage { get; set; }

        public DamageType DamageType { get; set; }

        public HeroTakedDamageEventArgs(Features.Hero hero, Features.Hero attacker, int damage, DamageType damageType)
        {
            Hero = hero;
            Attacker = attacker;
            Damage = damage;
            DamageType = damageType;
        }

    }
}
