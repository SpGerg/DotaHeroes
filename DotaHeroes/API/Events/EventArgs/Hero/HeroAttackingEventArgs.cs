﻿using DotaHeroes.API.Enums;
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
    public class HeroAttackingEventArgs : IHeroEvent, IDamage, IDeniableEvent
    {
        public Features.Hero Hero { get; set; }

        public Features.Hero Target { get; set; }

        public int Damage { get; set; }

        public DamageType DamageType { get; set; }

        public bool IsAllowed { get; set; }

        public HeroAttackingEventArgs(Features.Hero hero, Features.Hero target, int damage, bool isAllowed, DamageType damageType)
        {
            Hero = hero;
            Target = target;
            Damage = damage;
            IsAllowed = isAllowed;
            DamageType = damageType;
        }
    }
}
