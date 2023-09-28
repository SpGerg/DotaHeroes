using Exiled.API.Features;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DotaHeroes.API.Features
{
    public class CooldownInfo
    {
        public string Name { get; set; }

        public int Duration { get; set; }

        public int Cooldown {
            get
            {
                if (Time.time - useTime > Duration)
                {
                    return 0;
                }

                return Duration - (int)((int)Time.time - useTime);
            }
        }

        public bool IsReady
        {
            get
            {
                if (Time.time - useTime > Duration)
                {
                    return true;
                }

                return false;
            }
        }

        private float useTime { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CooldownInfo" /> class.
        /// </summary>
        /// <param name="name"><inheritdoc cref="Name" /></param>
        /// <param name="duration"><inheritdoc cref="Duration" /></param>
        public CooldownInfo(string name, int duration)
        {
            Name = name;
            Duration = duration;
        }

        /// <summary>
        /// Run. RUN.
        /// </summary>
        public void Run()
        {
            useTime = Time.time;
        }
    }
}
