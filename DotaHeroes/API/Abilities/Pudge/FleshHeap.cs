using CommandSystem;
using DotaHeroes.API.Effects.Pudge;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
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
    public class FleshHeap : ActiveAbility
    {
        public override string Name => "Flesh heap";

        public override string Command { get; set; } = "fleshheap";

        public override string Description => "Flesh heap";

        public override string Lore => "Flesh heap";

        public override AbilityType AbilityType => AbilityType.Active;

        public override TargetType TargetType => TargetType.None;

        public override int MaxLevel => 4;

        public FleshHeap() : base() { }

        public override void LevelUp(Hero hero)
        {
            var effect = hero.GetEffects().FirstOrDefault(_effect => _effect is Effects.Pudge.FleshHeap);

            if (effect != default)
            {
                effect.IsVisible = true;
            }

            base.LevelUp(hero);
        }

        public override bool Execute(Hero hero, ArraySegment<string> arguments, out string response)
        {
            hero.EnableEffect(new FleshHeapShield(hero.Player));

            response = "Flesh heap enabled.";

            return true;
        }
    }
}
