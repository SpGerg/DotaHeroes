using DotaHeroes.API.Enums;
using DotaHeroes.API.Events.EventArgs.Hero;
using DotaHeroes.API.Interfaces;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Features
{
    public class Effects
    {
        public Hero Owner { get; }

        protected List<Effect> ActiveEffects { get; set; }

        public Effects(Hero owner)
        {
            Owner = owner;
            ActiveEffects = new List<Effect>();
        }

        
    }
}
