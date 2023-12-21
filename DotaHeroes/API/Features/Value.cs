namespace DotaHeroes.API.Features
{
    public class Value
    {
        public double CoolValue { get; set; }

        public bool IsPercent { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Features.Value" /> class.
        /// </summary>
        public Value() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Features.Value" /> class.
        /// </summary>
        /// <param name="owner"><inheritdoc cref="Owner" /></param>
        public Value(double coolValue, bool isPercent)
        {
            CoolValue = coolValue;
            IsPercent = isPercent;
        }
    }
}
