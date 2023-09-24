using DotaHeroes.API.Events.EventArgs.Hero;
using DotaHeroes.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.Events.Internal
{
    internal class HeroHandler
    {
        internal static void OnHeroTakedDamage(HeroTakedDamageEventArgs ev)
        {
            var effect = ev.Hero.GetEffects().FirstOrDefault(effect => effect is IDamageBlock);

            if (effect == default)
            {
                return;
            }

            var damageBlock = effect as IDamageBlock;
            ev.Damage = API.Features.Utils.BlockDamage(ev.Damage, ev.DamageType, damageBlock.DamageBlock, damageBlock.DamageTypesToBlock);
        }
    }
}
