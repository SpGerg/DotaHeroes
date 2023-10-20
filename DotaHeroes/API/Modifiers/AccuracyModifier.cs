using DotaHeroes.API.Interfaces;

namespace DotaHeroes.API.Modifiers
{
    public class AccuracyModifier : IAccuracyModifier
    {
        public decimal Accuracy { get; set; }

        public AccuracyModifier(decimal accuracy)
        {
            Accuracy = accuracy;
        }
    }
}
