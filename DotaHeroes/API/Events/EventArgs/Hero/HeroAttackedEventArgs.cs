using DotaHeroes.API.Enums;
using DotaHeroes.API.Events.EventArgs.Interfaces;
using DotaHeroes.API.Interfaces;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Events.EventArgs.Hero
{
    public class HeroAttackedEventArgs : IHeroEvent, IDamage
    {
        public Features.Hero Hero { get; }

        public Features.Hero Target { get; }

        public int Damage { get; set; }

        public DamageType DamageType { get; set; }

        public HeroAttackedEventArgs(Features.Hero hero, Features.Hero target, int damage, DamageType damageType)
        {
            Hero = hero;
            Target = target;
            Damage = damage;
            DamageType = damageType;
        }
    }
}
