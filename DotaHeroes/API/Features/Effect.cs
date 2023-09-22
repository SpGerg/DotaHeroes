using DotaHeroes.API.Enums;
using DotaHeroes.API.Interfaces;
using Exiled.API.Features;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Features
{
    public abstract class Effect
    {
        public abstract string Name { get; }

        public abstract string Description { get; protected set; }

        public abstract EffectClassType EffectClassType { get; }

        public virtual DispelType DispelType { get; set; } = DispelType.None;

        public Player Owner { get; }

        public Hero Hero { get; }

        public bool IsVisible { get; set; } = true;

        public Effect() { }

        public Effect(Player owner)
        {
            Owner = owner;
            Hero = API.GetHeroOrDefault(owner.UserId);
        }

        public virtual bool Enable()
        {
            if (this is IEffectDuration)
            {
                Timing.CallDelayed((float)(this as IEffectDuration).Duration, () =>
                {
                    Hero.DisableEffect(this);
                });
            }

            return true;
        }

        public virtual bool Execute()
        {
            return true;
        }

        public virtual bool Dispel()
        {
            return false;
        }

        public virtual bool Disable()
        {
            return true;
        }

        public override string ToString()
        {
            if (EffectClassType == EffectClassType.Negative)
            {
                return $"<color=Red>{Name}</color>";
            }
            else if (EffectClassType == EffectClassType.Positive)
            {
                return $"<color=Green>{Name}</color>";
            }
            else
            {
                return Name;
            }
        }
    }
}
