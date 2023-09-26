using DotaHeroes.API.Events.EventArgs.Interfaces;
using DotaHeroes.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Events.EventArgs.Hero
{
    public class HeroDisabledEffectEventArgs : IHeroEvent
    {
        public Features.Hero Hero { get; }

        public Effect Effect { get; }

        public HeroDisabledEffectEventArgs(Features.Hero hero, Effect effect)
        {
            Hero = hero;
            Effect = effect;
        }
    }
}
