using DotaHeroes.API.Enums;
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
    public class HeroDispellingEventArgs : IHeroEvent, IDeniableEvent
    {
        public Features.Hero Hero { get; set; }

        public Features.Hero Dispeller { get; set; }

        public List<Effect> EffectsToDispel { get; set; }

        public DispelType DispelType { get; set; }

        public bool IsAllowed { get; set; }

        public HeroDispellingEventArgs(Features.Hero hero, Features.Hero dispeller, List<Effect> effectsToDispel, DispelType dispelType, bool isAllowed)
        {
            Hero = hero;
            Dispeller = dispeller;
            EffectsToDispel = effectsToDispel;
            DispelType = dispelType;
            IsAllowed = isAllowed;
        }
    }
}
