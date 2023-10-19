using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Effects
{
    public class Silence : Effect
    {
        public override string Name => "Silence";

        public override string Description { get; protected set; } = "Silence";

        public override EffectClassType EffectClassType => EffectClassType.Negative;

        public override DispelType DispelType { get; set; } = DispelType.Basic;
    }
}
