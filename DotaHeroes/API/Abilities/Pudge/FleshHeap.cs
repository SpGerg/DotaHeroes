using CommandSystem;
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
    public class FleshHeap : ActiveAbility
    {
        public override string Command => "fleshheap";

        public override string[] Aliases => Array.Empty<string>();

        public override string Name => "Flesh heap";

        public override string Description => "Flesh heap";

        public override string Lore => "Flesh heap";

        public override AbilityType AbilityType => AbilityType.Active;

        public override TargetType TargetType => TargetType.None;

        public override int MaxLevel => 4;

        public FleshHeap() : base() { }

        public FleshHeap(Player player) : base(player) { }

        public override bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            throw new NotImplementedException();
        }
    }
}
