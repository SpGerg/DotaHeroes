using DotaHeroes.API.Interfaces;

namespace DotaHeroes.API.Modifiers
{
    public class BlindModifier : IBlindModifier
    {
        public decimal Blind { get; set; }

        public BlindModifier(decimal blind)
        {
            Blind = blind;
        }
    }
}
