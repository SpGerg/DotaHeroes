using DotaHeroes.API.Events.EventArgs.Interfaces;
using DotaHeroes.API.Features;
using Exiled.Events.EventArgs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Events.EventArgs.Hero
{
    public class HeroReceivingEffectEventArgs : IHeroEvent, IDeniableEvent
    {
        public Features.Hero Hero { get; }

        public Effect Effect { get; }

        public bool IsAllowed { get; set; }

        public HeroReceivingEffectEventArgs(Features.Hero hero, Effect effect, bool isAllowed)
        {
            Hero = hero;
            Effect = effect;
            IsAllowed = isAllowed;
        }
    }
}
