using DotaHeroes.API.Events.EventArgs.Interfaces;
using Exiled.Events.EventArgs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Events.EventArgs.Hero
{
    public class HeroRespawningEventArgs : IHeroEvent, IDeniableEvent
    {
        public Features.Hero Hero { get; set; }

        public Features.Hero Killer { get; set; }

        public bool IsAllowed { get; set; }

        public HeroRespawningEventArgs(Features.Hero hero, Features.Hero killer, bool isAllowed)
        {
            Hero = hero;
            Killer = killer;
            IsAllowed = isAllowed;
        }
    }
}
