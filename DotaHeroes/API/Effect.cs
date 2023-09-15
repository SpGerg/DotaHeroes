using DotaHeroes.API.Enums;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API
{
    public abstract class Effect
    {
        public Player Owner { get; }

        public abstract string Name { get; }

        public abstract string Description { get; }

        public abstract EffectClassType EffectClassType { get; }

        public virtual DispelType DispelType { get; set; } = DispelType.None;

        public Effect() { }

        public Effect(Player owner)
        {
            Owner = owner;
        }

        public virtual bool Enable()
        {
            return true;
        }

        public virtual bool Execute()
        {
            return true;
        }

        public virtual bool Disable()
        {
            return true;
        }
    }
}
