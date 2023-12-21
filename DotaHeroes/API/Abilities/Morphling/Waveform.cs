using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Abilities.Morphling
{
    public class Waveform : ActiveAbility
    {
        public override string Name => "Waveform";

        public override string Slug => "waveform";

        public override string Description => "Waveform";

        public override string Lore => "Waveform";

        public override AbilityType AbilityType => AbilityType.Active;

        public override TargetType TargetType => TargetType.ToPoint;

        public Waveform(Hero hero) : base(hero) { }

        protected override bool Execute(ArraySegment<string> arguments, out string response)
        {
            throw new NotImplementedException();
        }

        public override Ability Create(Hero hero)
        {
            throw new NotImplementedException();
        }
    }
}
