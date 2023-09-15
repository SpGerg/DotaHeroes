using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Features
{
    public class Cooldown
    {
        public int Duration { get; set; }

        public bool IsCompleted { get; set; } = true;

        public Cooldown(int duration)
        {
            Duration = duration;
        }

        public void Run()
        {
            IsCompleted = false;

            Timing.CallDelayed(Duration, () =>
            {
                IsCompleted = true;
            });
        }
    }
}
