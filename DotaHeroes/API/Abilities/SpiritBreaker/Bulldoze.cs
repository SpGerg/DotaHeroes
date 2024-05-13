using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Interfaces;
using System;
using System.Collections.Generic;

namespace DotaHeroes.API.Abilities.SpiritBreaker
{
    public class Bulldoze : ActiveAbility, ILevelValues
    {
        public override string Name => "Bulldoze";

        public override string Slug => "bulldoze";

        public override string Description => "Bulldoze";

        public override string Lore => "Bulldoze";

        public override AbilityType AbilityType => AbilityType.Active;

        public override TargetType TargetType => TargetType.None;

        public Dictionary<string, List<double>> Values { get; } = Plugin.Instance.Config.Abilites["bulldoze"].Values;

        public int MaxLevel { get; set; } = 4;
        public int MinLevel { get; set; } = 0;

        public IReadOnlyList<int> HeroLevelToLevelUp { get; } = Features.Utils.EmptyLevelsList;

        public static string SoundsPath = Plugin.Instance.SoundsPath + "\\spirit_breaker\\bulldoze";

        public Bulldoze(Hero hero) : base(hero) { }

        protected override bool Execute(ArraySegment<string> arguments, out string response)
        {
            var effect = new Effects.SpiritBreaker.Bulldoze(Owner);
            effect.EffectResistance = Values["extra_effect_resistance"][Level];

            Owner.EnableEffect(effect, (float)Values["duration"][Level]);

            Features.Audio.Play(Owner.Player.Position, SoundsPath + "\\activate.ogg", 75f, false, Owner.Player);

            response = "Bulldoze has been activated.";
            return true;
        }

        public override Ability Create(Hero hero)
        {
            return new Bulldoze(hero);
        }
    }
}
