using DotaHeroes.API.Enums;

namespace DotaHeroes.API.Interfaces
{
    /// <summary>
    /// Damage and damage type
    /// </summary>
    public interface IDamage
    {
        double Damage { get; set; }

        DamageType DamageType { get; set; }
    }
}
