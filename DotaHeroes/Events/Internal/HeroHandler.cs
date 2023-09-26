using DotaHeroes.API.Events.EventArgs.Hero;
using DotaHeroes.API.Features;
using DotaHeroes.API.Interfaces;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.Events.Internal
{
    internal class HeroHandler
    {
        internal static void BlockingDamage(HeroTakingDamageEventArgs ev)
        {
            var damageBlockType = typeof(IDamageBlock);

            var damageBlocks = ev.Hero.GetEffects().Where(effect => damageBlockType.IsAssignableFrom(effect.GetType()));

            int total_damage = ev.Damage;

            foreach (var effect in damageBlocks)
            {
                var effectDamageBlock = effect as IDamageBlock;
                total_damage = API.Features.Utils.BlockDamage(total_damage, ev.DamageType, effectDamageBlock.DamageBlock, effectDamageBlock.DamageTypesToBlock);
            }

            ev.Damage = total_damage;
        }

        internal static void UpdateHudOnTakedDamage(HeroTakedDamageEventArgs ev)
        {
            Hud.Update();
        }

        internal static void UpdateHudOnHealed(HeroHealedEventArgs ev)
        {
            Hud.Update();
        }

        internal static void UpdateHudHeathOnReceivingEffect(HeroReceivingEffectEventArgs ev)
        {
            Hud.Update();
        }

        internal static void UpdateHudHeathOnDisabledEffect(HeroDisabledEffectEventArgs ev)
        {
            Hud.Update();
        }
    }
}
