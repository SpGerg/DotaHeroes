namespace DotaHeroes.API.Interfaces
{
    public interface IAccuracyModifier : IModifier
    {
        decimal Accuracy { get; set; }
    }
}
