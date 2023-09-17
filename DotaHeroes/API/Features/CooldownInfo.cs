using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Features
{
    public class CooldownInfo
    {
        public string Name { get; set; }

        public int Duration { get; set; }

        public bool IsCompleted { get; set; }

        public CooldownInfo(string name, int duration, bool isCompleted)
        {
            Name = name;
            Duration = duration;
            IsCompleted = isCompleted;
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
