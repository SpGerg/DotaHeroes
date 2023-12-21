using DotaHeroes.API.Interfaces;

namespace DotaHeroes.API.Modifiers
{
    public class BlindModifier : IBlindModifier
    {
        public double Blind { get; set; }

        public BlindModifier(double blind)
        {
            Blind = blind;
        }
    }
}
