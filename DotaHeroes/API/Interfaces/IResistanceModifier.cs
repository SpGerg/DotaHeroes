
namespace DotaHeroes.API.Interfaces
{
    /// <summary>
    /// Resistance modifier. It can be used for items and abilties (like + 15% magic resistance and other)
    /// </summary>
    public interface IResistanceModifier : IModifier
    {
        double MagicResistance { get; set; }

        double EffectResistance { get; set; }
    }
}
