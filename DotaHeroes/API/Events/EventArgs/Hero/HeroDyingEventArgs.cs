using DotaHeroes.API.Events.EventArgs.Interfaces;
using Exiled.Events.EventArgs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Events.EventArgs.Hero
{
    public class HeroDyingEventArgs : IHeroEvent, IDeniableEvent
    {
        public Features.Hero Hero { get; set; }

        public bool IsAllowed { get; set; }

        public HeroDyingEventArgs(Features.Hero hero, bool isAllowed)
        {
            Hero = hero;
            IsAllowed = isAllowed;
        }
    }
}
