using DotaHeroes.API.Effects.Pudge;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotaHeroes.API.Abilities.Pudge
{
    public class FleshHeap : ActiveAbility, ILevelValues
    {
        public override string Name => "Flesh heap";

        public override string Slug => "flesh_heap";

        public override string Description => "Flesh heap";

        public override string Lore => "Flesh heap";

        public override AbilityType AbilityType => AbilityType.Active;

        public override TargetType TargetType => TargetType.None;

        public Dictionary<string, List<double>> Values { get; } = Plugin.Instance.Config.Abilites["flesh_heap"].Values;

        public int MaxLevel { get; set; } = 4;

        public int MinLevel { get; set; } = 0;

        public IReadOnlyList<int> HeroLevelToLevelUp { get; set; } = Features.Utils.EmptyLevelsList;

        public static string SoundsPath = Plugin.Instance.SoundsPath + "\\pudge\\flesh_heap";

        public FleshHeap(Hero hero) : base(hero) { }

        public override void LevelUp()
        {
            var effect = Owner.GetEffects().FirstOrDefault(_effect => _effect is Effects.Pudge.FleshHeap) as Effects.Pudge.FleshHeap;

            if (effect != default)
            {
                effect.IsVisible = true;
                effect.Stack++;
            }

            base.LevelUp();
        }

        protected override bool Execute(ArraySegment<string> arguments, out string response)
        {
            var blockDamage = Owner.EnableEffect(new FleshHeapShield(Owner)) as FleshHeapShield;
            blockDamage.DamageBlock = (int)Values["damage_block"][Level];

            response = "Flesh heap enabled.";

            return true;
        }

        public override Ability Create(Hero hero)
        {
            return new FleshHeap(hero);
        }
    }
}
