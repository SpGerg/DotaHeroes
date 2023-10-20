using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;

namespace DotaHeroes.API.Effects.SpiritBreaker
{
    public class ChargeOfDarknessSpeed : Effect
    {
        public override string Name => "Charge of darkness";

        public override string Description { get; protected set; } = "Charge of darkness";

        public override EffectClassType EffectClassType => EffectClassType.Positive;

        public override DispelType DispelType { get; set; } = DispelType.Dead;

        public sbyte ExtraSpeed { get; set; }

        public ChargeOfDarknessSpeed() : base() { }

        public ChargeOfDarknessSpeed(Hero owner) : base(owner) { }

        public override void Enable()
        {
            Owner.HeroStatistics.Speed.Speed += ExtraSpeed;

            base.Enable();
        }

        public override void Disable()
        {
            Owner.HeroStatistics.Speed.Speed -= ExtraSpeed;
            
            base.Disable();
        }
    }
}
