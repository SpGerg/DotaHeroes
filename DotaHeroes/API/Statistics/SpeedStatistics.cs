using CustomPlayerEffects;
using DotaHeroes.API.Features;
using Exiled.API.Features;
using MEC;
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
                speed = value;

                if (Hero != null && Hero.Player != null)
                {
                    if (speed < 0)
                    {
                        Hero.Player.EnableEffect<Disabled>();
                    }
                    else if (speed > 1)
                    {
                        Hero.Player.EnableEffect<MovementBoost>();
                        Hero.Player.ChangeEffectIntensity<MovementBoost>((byte)speed);

                        Hero.Player.DisableEffect<Disabled>();
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

            Timing.CallDelayed(0.5f, () => //For some reason when player spawn, effects not enabling. Im sorry for this shit lol
            {
                Speed = speed;

                Hud.Update();
            });
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
