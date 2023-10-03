using CommandSystem;
using DotaHeroes.API.Effects.Pudge;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Interfaces;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Abilities.Pudge
{
    [CommandHandler(typeof(ClientCommandHandler))]
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class FleshHeap : ActiveAbility, ILevelValues
    {
        public override string Name => "Flesh heap";

        public override string Description => "Flesh heap";

        public override string Lore => "Flesh heap";

        public override AbilityType AbilityType => AbilityType.Active;

        public override TargetType TargetType => TargetType.None;

        public Dictionary<string, List<float>> Values => Plugin.Instance.Config.Abilites["fleshheap"].Values;

        public int MaxLevel { get; set; } = 4;

        public int MinLevel { get; set; } = 0;

        public IReadOnlyList<int> HeroLevelToLevelUp { get; set; } = new List<int>();

        public FleshHeap() : base() { }

        public override void LevelUp(Hero hero)
        {
            var effect = hero.GetEffects().FirstOrDefault(_effect => _effect is Effects.Pudge.FleshHeap) as Effects.Pudge.FleshHeap;

            if (effect != default)
            {
                effect.IsVisible = true;
                effect.Stack++;
            }

            base.LevelUp(hero);
        }

        public override bool Execute(Hero hero, ArraySegment<string> arguments, out string response)
        {
            var blockDamage = hero.EnableEffect(new FleshHeapShield(hero)) as FleshHeapShield;
            blockDamage.DamageBlock = (int)Values["damage_block"][Level];

            response = "Flesh heap enabled.";

            return true;
        }
    }
}
