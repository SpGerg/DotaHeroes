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
            foreach (var effect in ev.Hero.GetEffects())
            {
                if (effect is IDamageBlock)
                {
                    var damageBlock = ((IDamageBlock)effect);
                    ev.Damage = API.Features.Utils.BlockDamage(ev.Damage, ev.DamageType, damageBlock.DamageBlock, damageBlock.DamageTypesToBlock);
                }
            }
        }
    }
}
