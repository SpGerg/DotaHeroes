namespace DotaHeroes.API.Interfaces
{
    /// <summary>
    /// Ability cost, mana and health.
    /// </summary>
    public interface ICost
    {
        int ManaCost { get; set; }

        int HealthCost { get; set; }
    }
}
