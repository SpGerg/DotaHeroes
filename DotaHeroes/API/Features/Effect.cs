using DotaHeroes.API.Enums;
using DotaHeroes.API.Interfaces;
using MEC;

namespace DotaHeroes.API.Features
{
    public abstract class Effect
    {
        public abstract string Name { get; }

        public abstract string Description { get; protected set; }

        public abstract EffectClassType EffectClassType { get; }

        public virtual DispelType DispelType { get; set; } = DispelType.None;

        public virtual int Stack { get; set; } = -1;

        public Hero Owner { get; }

        public Hero Hero { get; }

        public bool IsVisible { get; set; } = true;

        public bool IsActive { get; set; }

        public Effect() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Effect" /> class.
        /// </summary>
        /// <param name="owner"><inheritdoc cref="Owner" /></param>
        public Effect(Hero owner)
        {
            Owner = owner;
            Hero = API.GetHeroOrDefault(owner.Player.Id);
        }

        /// <summary>
        /// Enable effect.
        /// </summary>
        public virtual void Enable()
        {
            if (this is IEffectDuration effectDuration)
            {
                if (effectDuration.Duration > 0)
                {
                    Timing.CallDelayed(effectDuration.Duration, () =>
                    {
                        Hero.DisableEffect(this);
                    });
                }
            }

            IsActive = true;
        }

        /// <summary>
        /// Execute effect.
        /// </summary>
        public virtual void Execute() { }

        /// <summary>
        /// Dispel effect.
        /// </summary>
        public virtual void Dispel() { }

        /// <summary>
        /// Disable effect.
        /// </summary>
        public virtual void Disable()
        {
            IsActive = false;
        }

        /// <summary>
        /// To string.
        /// </summary>
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
