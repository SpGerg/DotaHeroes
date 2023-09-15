using Exiled.API.Features;
using Exiled.Events.EventArgs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Events.EventArgs.Hero
{
    public class HeroAttackingEventArgs : IPlayerEvent, IDeniableEvent
    {
        public Player Player { get; set; }

        public Player Target { get; set; }

        public bool IsAllowed { get; set; }
    }
}
