using DotaHeroes.API.Abilities.Items;
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

        public Dictionary<string, List<decimal>> Values { get; } = Plugin.Instance.Config.Abilites["bulldoze"].Values;

        public int MaxLevel { get; set; } = 4;
        public int MinLevel { get; set; } = 0;

        public IReadOnlyList<int> HeroLevelToLevelUp { get; } = Features.Utils.EmptyLevelsList;

        public static string SoundsPath = Plugin.Instance.SoundsPath + "\\spirit_breaker\\bulldoze";

        protected override bool Execute(Hero hero, ArraySegment<string> arguments, out string response)
        {
            var effect = new Effects.SpiritBreaker.Bulldoze(hero);
            effect.EffectResistance = Values["extra_effect_resistance"][Level];

            hero.EnableEffect(effect, (float)Values["duration"][Level]);

            Audio.Play(hero.Player.Position, SoundsPath + "\\activate.ogg", 75f, false, hero.Player);

            response = "Bulldoze has been activated.";
            return true;
        }

        public override Ability Create()
        {
            return new Bulldoze();
        }
    }
}
