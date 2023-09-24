using DotaHeroes.API.Events.EventArgs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Events.EventArgs.Hero
{
    public class HeroDiedEventArgs : IHeroEvent
    {
        public Features.Hero Hero { get; set; }

        public HeroDiedEventArgs(Features.Hero hero)
        {
            Hero = hero;
        }
    }
}
