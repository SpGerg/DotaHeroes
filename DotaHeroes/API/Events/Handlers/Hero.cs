using DotaHeroes.API.Events.EventArgs;
using DotaHeroes.API.Events.EventArgs.Hero;
using Exiled.Events.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Events.Handlers
{
    public static class Hero
    {
        public static Event<HeroAttackingEventArgs> Attacking { get; set; } = new();

        public static Event<HeroAttackedEventArgs> Attacked { get; set; } = new();

        public static Event<HeroTakingDamageEventArgs> TakingDamage { get; set; } = new();

        public static Event<HeroTakedDamageEventArgs> TakedDamage { get; set; } = new();

        public static void OnHeroAttacking(HeroAttackingEventArgs ev) => Attacking.InvokeSafely(ev);

        public static void OnHeroAttacked(HeroAttackedEventArgs ev) => Attacked.InvokeSafely(ev);

        public static void OnHeroTakingDamage(HeroTakingDamageEventArgs ev) => TakingDamage.InvokeSafely(ev);

        public static void OnHeroTakedDamage(HeroTakedDamageEventArgs ev) => TakedDamage.InvokeSafely(ev);
    }
}
