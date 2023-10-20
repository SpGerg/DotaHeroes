using CustomPlayerEffects;
using DotaHeroes.API.Features;
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

                if (Hero != null && Hero.Player != null)
                {
                    Hero.Player.DisableEffect<MovementBoost>();

                    if (speed < 0)
                    {
                        Hero.Player.EnableEffect<Disabled>();
                    }
                    else if (speed == 0)
                    {
                        Hero.Player.DisableEffect<Disabled>();
                    }
                    else if (speed > 1)
                    {
                        Hero.Player.DisableEffect<Disabled>();

                        Hero.Player.EnableEffect<MovementBoost>();
                        Hero.Player.ChangeEffectIntensity<MovementBoost>((byte)speed);
                    }
                }
            }
        }

        public Hero Hero { get; private set; }

        private sbyte speed;

        public SpeedStatistics()
        {
            Hero = null;
        }

        public SpeedStatistics(Hero hero)
        {
            Hero = hero;
        }

        public SpeedStatistics(Hero hero, sbyte speed)
        {
            Hero = hero;
            Speed = speed;
        }

        public void SetHeroIfNull(Hero hero)
        {
            if (Hero != null) return;

            Hero = hero;
        }

        public override string ToString()
        {
            return $"Speed: {Speed}";
        }
    }
}
