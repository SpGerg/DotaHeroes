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

        public float Duration { get; set; }

        public int Cooldown {
            get
            {
                if ((DateTime.Now - useTime).TotalSeconds > Duration)
                {
                    return 0;
                }

                return (int)Duration - (DateTime.Now - useTime).Seconds;
            }
        }

        public bool IsReady
        {
            get
            {
                if ((DateTime.Now - useTime).TotalSeconds > Duration)
                {
                    return true;
                }

                return false;
            }
        }

        private DateTime useTime { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CooldownInfo" /> class.
        /// </summary>
        /// <param name="name"><inheritdoc cref="Name" /></param>
        /// <param name="duration"><inheritdoc cref="Duration" /></param>
        public CooldownInfo(string name, float duration)
        {
            Name = name;
            Duration = duration;
        }

        /// <summary>
        /// Run. RUN.
        /// </summary>
        public void Run()
        {
            useTime = DateTime.Now;
        }
    }
}
