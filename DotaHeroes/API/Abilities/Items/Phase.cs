using DotaHeroes.API.Effects.Items;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Abilities.Items
{
    public class Phase : ActiveAbility, ILevelValues
    {
        public override string Name => "Phase";

        public override string Slug => "phase";

        public override string Description => "Phase";

        public override string Lore => "Phase";

        public override AbilityType AbilityType => AbilityType.Active;

        public override TargetType TargetType => TargetType.None;

        public Dictionary<string, List<decimal>> Values { get; } = Plugin.Instance.Config.Abilites["phase"].Values;

        public int MaxLevel { get; set; } = 1;

        public int MinLevel { get; set; } = 1;

        public IReadOnlyList<int> HeroLevelToLevelUp => throw new NotImplementedException();

        public Phase() : base() { }

        public override void LevelUp(Hero hero) { }

        protected override bool Execute(Hero hero, ArraySegment<string> arguments, out string response)
        {
            var phase = new PhaseSpeed(hero);
            //lol
            phase.ExtraSpeed = (sbyte)Values["extra_speed_" + hero.HeroClassType.ToString().ToLower()][Level];

            hero.EnableEffect(phase, (float)Values["duration"][Level]);

            response = "Phase boots was used";
            return true;
        }

        public override Ability Create()
        {
            return new Phase();
        }
    }
}
