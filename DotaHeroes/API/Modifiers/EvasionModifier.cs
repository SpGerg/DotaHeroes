using DotaHeroes.API.Interfaces;

namespace DotaHeroes.API.Modifiers
{
    public class EvasionModifier : IEvasionModifier
    {
        public decimal Evasion { get; set; }

        public EvasionModifier(decimal evasion)
        {
            Evasion = evasion;
        }
    }
}
