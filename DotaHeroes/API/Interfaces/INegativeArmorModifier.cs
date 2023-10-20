namespace DotaHeroes.API.Interfaces
{
    public interface INegativeArmorModifier : IModifier
    {
        decimal NegativeArmor { get; set; }
    }
}
