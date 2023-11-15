using DotaHeroes.API.Enums;
using DotaHeroes.API.Interfaces;
using MEC;
using NorthwoodLib.Pools;
using System;
using System.Text;

namespace DotaHeroes.API.Features
{
    public abstract class Effect
    {
        public abstract string Name { get; }

        public abstract string Slug { get; }

        public abstract string Description { get; protected set; }

        public abstract EffectClassType EffectClassType { get; }

        public virtual DispelType DispelType { get; set; } = DispelType.None;

        public virtual int Stack { get; set; } = -1;

        public virtual bool IsStacking { get; set; }

        public Hero Owner { get; }

        public bool IsVisible { get; set; } = true;

        public bool IsActive { get; set; }

        public float EffectDuration
        {
            get
            {
                if (enabledTime == null || this is not ILevelValues)
                {
                    return -1;
                }

                return (float)(DateTime.Now - enabledTime).TotalSeconds;
            }
        }

        private DateTime enabledTime;

        public Effect() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Effect" /> class.
        /// </summary>
        /// <param name="owner"><inheritdoc cref="Owner" /></param>
        public Effect(Hero owner)
        {
            Owner = owner;
        }

        /// <summary>
        /// Enable effect.
        /// </summary>
        public virtual void Enabled()
        {
            if (this is IEffectDuration effectDuration)
            {
                if (effectDuration.Duration > 0)
                {
                    Timing.CallDelayed(effectDuration.Duration, () =>
                    {
                        Owner.DisableEffect(this);
                    });
                }
            }

            enabledTime = DateTime.Now;
            IsActive = true;
        }

        /// <summary>
        /// Execute effect.
        /// </summary>
        public virtual void Executed() { }

        /// <summary>
        /// Dispel effect.
        /// </summary>
        public virtual void Dispelled() { }

        /// <summary>
        /// Disable effect.
        /// </summary>
        public virtual void Disabled()
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

        /// <summary>
        /// To string hud.
        /// </summary>
        public virtual string ToStringHud(Hero hero)
        {
            StringBuilder stringBuilder = StringBuilderPool.Shared.Rent();
            stringBuilder.AppendLine("Name: " + Name);

            var duration = EffectDuration;

            if (duration != -1)
            {
                stringBuilder.AppendLine("Duration: " + duration);
            }
            

            return StringBuilderPool.Shared.ToStringReturn(stringBuilder);
        }
    }
}
