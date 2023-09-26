using DotaHeroes.API.Events.EventArgs.Interfaces;
using Exiled.Events.EventArgs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Events.EventArgs.Hero
{
    public class HeroHealingEventArgs : IHeroEvent, IDeniableEvent
    {
        public Features.Hero Hero { get; }

        public Features.Hero Healer { get; }

        public float Heal { get; set; }

        public bool IsAllowed { get; set; }

        public HeroHealingEventArgs(Features.Hero hero, Features.Hero healer, float heal, bool isAllowed)
        {
            Hero = hero;
            Healer = healer;
            Heal = heal;
            IsAllowed = isAllowed;
        }
    }
}
