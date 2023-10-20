using DotaHeroes.API.Interfaces;

namespace DotaHeroes.API.Modifiers
{
    public class ResistanceModifier : IResistanceModifier
    {
        public decimal MagicResistance { get; set; }

        public decimal EffectResistance { get; set; }

        public ResistanceModifier(decimal magicResistance, decimal effectResistance)
        {
            MagicResistance = magicResistance;
            EffectResistance = effectResistance;
        }
    }
}
