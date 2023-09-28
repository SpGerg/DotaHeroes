using CustomPlayerEffects;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Interfaces;
using Exiled.API.Features;
using Exiled.API.Features.Toys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Effects.Pudge
{
    public class Rot : Features.Effect, IDamage
    {
        public override string Name => "Rot";

        public override string Description { get; protected set; } = "Rot";

        public override EffectClassType EffectClassType => EffectClassType.Negative;

        public override DispelType DispelType { get; set; } = DispelType.NotDispelling;

        private DamageOverTime DamageOverTime;

        public int Damage { get; set; }

        public DamageType DamageType { get; set; }

        public Rot() : base() { }

        public Rot(Hero owner) : base(owner)
        {
            DamageOverTime = new DamageOverTime(owner, 30, DamageType.Magical, -1, 1f);
        }

        public override bool Enable()
        {
            Hero.HeroStatistics.Speed.Speed -= 10;
            DamageOverTime.Damage = Damage;
            DamageOverTime.DamageType = DamageType;
            DamageOverTime.Run();
             
            base.Enable();

            return true;
        }

        public override bool Disable()
        {
            Hero.HeroStatistics.Speed.Speed += 10;
            DamageOverTime.IsEnabled = false;

            base.Disable();

            return true;
        }
    }
}
