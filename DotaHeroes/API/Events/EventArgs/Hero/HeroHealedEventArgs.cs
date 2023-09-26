using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Events.EventArgs.Hero
{
    public class HeroHealedEventArgs
    {
        public Features.Hero Hero { get; }

        public Features.Hero Healer { get; }

        public float Heal { get; set; }

        public HeroHealedEventArgs(Features.Hero hero, Features.Hero healer, float heal)
        {
            Hero = hero;
            Healer = healer;
            Heal = heal;
        }
    }
}
