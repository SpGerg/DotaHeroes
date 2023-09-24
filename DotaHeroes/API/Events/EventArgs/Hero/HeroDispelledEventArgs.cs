using DotaHeroes.API.Enums;
using DotaHeroes.API.Events.EventArgs.Interfaces;
using DotaHeroes.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Events.EventArgs.Hero
{
    public class HeroDispelledEventArgs : IHeroEvent
    {
        public Features.Hero Hero { get; set; }

        public Features.Hero Dispeller { get; set; }

        public List<Effect> EffectsToDispel { get; set; }

        public DispelType DispelType { get; set; }

        public HeroDispelledEventArgs(Features.Hero hero, Features.Hero dispeller, List<Effect> effectsToDispel, DispelType dispelType)
        {
            Hero = hero;
            Dispeller = dispeller;
            EffectsToDispel = effectsToDispel;
            DispelType = dispelType;
        }
    }
}
