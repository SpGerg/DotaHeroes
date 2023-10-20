using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Interfaces;
using Exiled.API.Features;

namespace DotaHeroes.API.Effects.Pudge
{
    public class Rot : Features.Effect, IDamage
    {
        public override string Name => "Rot";

        public override string Description { get; protected set; } = "Rot";

        public override EffectClassType EffectClassType => EffectClassType.Negative;

        public override DispelType DispelType { get; set; } = DispelType.NotDispelling;

        public Hero Attacker { get; set; }

        public decimal Damage { get; set; }

        public DamageType DamageType { get; set; }

        private DamageOverTime DamageOverTime;

        public Rot() : base() { }

        public Rot(Hero owner) : base(owner)
        {
            DamageOverTime = new DamageOverTime(owner, 30, DamageType.Magical, -1, 1f, Attacker);
        }

        public override void Enable()
        {
            Hero.HeroStatistics.Speed.Speed -= 10;
            DamageOverTime.Damage = Damage;
            Log.Info("п");
            DamageOverTime.DamageType = DamageType;
            DamageOverTime.Run();
             
            base.Enable();
        }

        public override void Disable()
        {
            Hero.HeroStatistics.Speed.Speed += 10;
            DamageOverTime.IsEnabled = false;

            base.Disable();
        }
    }
}
