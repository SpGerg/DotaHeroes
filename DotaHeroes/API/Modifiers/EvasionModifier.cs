using DotaHeroes.API.Interfaces;

namespace DotaHeroes.API.Modifiers
{
    public class EvasionModifier : IEvasionModifier
    {
        public double Evasion { get; set; }

        public EvasionModifier(double evasion)
        {
            Evasion = evasion;
        }
    }
}
