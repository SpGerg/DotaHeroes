﻿using DotaHeroes.API.Events.EventArgs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Events.EventArgs.Hero
{
    public class HeroRespawnedEventArgs : IHeroEvent
    {
        public Features.Hero Hero { get; }

        public Features.Hero Killer { get; }

        public HeroRespawnedEventArgs(Features.Hero hero, Features.Hero killer)
        {
            Hero = hero;
            Killer = killer;
        }
    }
}
