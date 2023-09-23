using CustomPlayerEffects;
using DotaHeroes.API.Features;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DotaHeroes.API.Statistics
{
    public class SpeedStatistics
    {
        public sbyte Speed {
            get
            {
                return speed;
            }
            set
            {
                speed = (sbyte)Mathf.Clamp(value, sbyte.MinValue, sbyte.MaxValue);

                if (Hero.Player != null)
                {
                    if (speed < 0)
                    {
                        Hero.Player.EnableEffect<Disabled>();
                    }
                    else
                    {
                        Hero.Player.DisableEffect<Disabled>();
                    }
                }
            }
        }

        public Hero Hero { get; }

        private sbyte speed;

        public SpeedStatistics(Hero hero)
        {
            Hero = hero;
        }

        public SpeedStatistics(Hero hero, sbyte speed)
        {
            Hero = hero;
            Speed = speed;
        }

        public override string ToString()
        {
            return $"Speed: {Speed}";
        }
    }
}
