using DotaHeroes.API.Interfaces;

namespace DotaHeroes.API.Modifiers
{
    public class SpeedModifier : ISpeedModifier
    {
        public sbyte Speed { get; set; }

        public SpeedModifier(sbyte speed)
        {
            Speed = speed;
        }
    }
}
