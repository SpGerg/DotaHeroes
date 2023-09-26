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
    public class FleshHeap : ActiveAbility, IValues
    {
        public override string Name => "Flesh heap";

        public override string Command { get; set; } = "fleshheap";

        public override string Description => "Flesh heap";

        public override string Lore => "Flesh heap";

        public override AbilityType AbilityType => AbilityType.Active;

        public override TargetType TargetType => TargetType.None;

        public override int MaxLevel => 4;

        public IReadOnlyDictionary<string, List<float>> Values => new Dictionary<string, List<float>>()
        {
            { "damage_block", new List<float> { 8, 14, 20, 26 } },
            { "mana_cost", new List<float> { 120, 130, 140, 150 } },
            { "cooldown", new List<float> { 12, 11, 9, 8 } },
        };

        public FleshHeap() : base() { }

        public override void LevelUp(Hero hero)
        {
            var effect = hero.GetEffects().FirstOrDefault(_effect => _effect is Effects.Pudge.FleshHeap) as Effects.Pudge.FleshHeap;

            if (effect != default)
            {
                effect.IsVisible = true;
                effect.Count++;
            }

            base.LevelUp(hero);
        }

        public override bool Execute(Hero hero, ArraySegment<string> arguments, out string response)
        {
            var blockDamage = hero.EnableEffect(new FleshHeapShield(hero.Player)) as FleshHeapShield;
            blockDamage.DamageBlock = (int)Values["damage_block"][Level];

            response = "Flesh heap enabled.";

            return true;
        }
    }
}
