using DotaHeroes.API.Effects;
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

            var damageBlocks = ev.Hero.GetEffects().Where(effect => damageBlockType.IsAssignableFrom(effect.GetType())); //For some reason, checking "effect is IDamageBlock" is not work.

            double total_damage = ev.Damage;

            foreach (var effect in damageBlocks)
            {
                var effectDamageBlock = effect as IDamageBlock;
                total_damage = API.Features.Utils.BlockDamage(total_damage, ev.DamageType, effectDamageBlock);
            }

            ev.Damage = total_damage;
        }

        internal static void GiveExperienceAndMoneyFromKill(HeroDiedEventArgs ev)
        {
            if (ev.Killer == null) return;

            ev.Killer.Experience += Plugin.Instance.Config.ExpFromKill;
            ev.Killer.Money += Plugin.Instance.Config.MoneyFromKill;
        }

        internal static void AddFleshHeapStackOnDied(HeroDiedEventArgs ev)
        {
            foreach (var hero in API.DTAPI.GetHeroes().Values)
            {
                if (hero.IsHeroDead) continue;

                var effect = hero.GetEffectOrDefault<API.Effects.Pudge.FleshHeap>();

                if (effect == default) continue;

                if (Vector3.Distance(ev.Hero.Player.Position, hero.Player.Position) < 10)
                {
                    effect.Executed();
                    return;
                }
            }
        }
        
        internal static void Silence(HeroExecutingAbilityEventArgs ev)
        {
            if (!ev.Hero.TryGetEffect(out Silence effect)) return;

            ev.IsAllowed = false;
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
