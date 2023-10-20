using System.Collections.Generic;

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
