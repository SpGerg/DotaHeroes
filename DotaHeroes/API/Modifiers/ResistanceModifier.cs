using DotaHeroes.API.Interfaces;

namespace DotaHeroes.API.Modifiers
{
    public class ResistanceModifier : IResistanceModifier
    {
        public double MagicResistance { get; set; }

        public double EffectResistance { get; set; }

        public ResistanceModifier(double magicResistance, double effectResistance)
        {
            MagicResistance = magicResistance;
            EffectResistance = effectResistance;
        }
    }
}
