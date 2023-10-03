using DotaHeroes.API.Events.EventArgs.Hero;
using DotaHeroes.API.Features;
using DotaHeroes.API.Interfaces;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DotaHeroes.Events.Internal
{
    internal class HeroHandler
    {
        internal static void BlockingDamage(HeroTakingDamageEventArgs ev)
        {
            var damageBlockType = typeof(IDamageBlock);

            var damageBlocks = ev.Hero.GetEffects().Where(effect => damageBlockType.IsAssignableFrom(effect.GetType()));

            decimal total_damage = ev.Damage;

            foreach (var effect in damageBlocks)
            {
                var effectDamageBlock = effect as IDamageBlock;
                total_damage = API.Features.Utils.BlockDamage(total_damage, ev.DamageType, effectDamageBlock);
            }

            ev.Damage = total_damage;
        }

        internal static void AddFleshHeapStackOnDied(HeroDiedEventArgs ev)
        {
            foreach (var hero in API.API.GetHeroes().Values)
            {
                if (hero.IsHeroDead) continue;

                var effect = hero.GetEffectOrDefault<API.Effects.Pudge.FleshHeap>();

                if (effect == default) continue;

                if (Vector3.Distance(ev.Hero.Player.Position, hero.Player.Position) < 10)
                {
                    effect.Execute();
                    return;
                }
            }
        }

        internal static void UpdateHudOnTakedDamage(HeroTakedDamageEventArgs ev)
        {
            Hud.Update(ev.Hero);
        }

        internal static void UpdateHudOnHealed(HeroHealedEventArgs ev)
        {
            Hud.Update(ev.Hero);
        }
    }
}
