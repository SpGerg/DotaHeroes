using DotaHeroes.API.Enums;
using DotaHeroes.API.Events.EventArgs.Hero;
using DotaHeroes.API.Features;
using DotaHeroes.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Abilities.Items
{
    public class DamageBlock : PassiveAbility, ILevelValues
    {
        public override string Name => "Damage block";

        public override string Slug => "damage_block";

        public override string Description => "Damage block";

        public override string Lore => "Damage block";

        public Dictionary<string, List<decimal>> Values { get; } = Plugin.Instance.Config.Abilites["phase"].Values;

        public int MaxLevel { get; set; } = 1;

        public int MinLevel { get; set; } = 1;

        public IReadOnlyList<int> HeroLevelToLevelUp => throw new NotImplementedException();

        public override AbilityType AbilityType => AbilityType.Passive;

        public override TargetType TargetType => TargetType.None;

        public DamageBlock() : base() { }

        public override void LevelUp(Hero hero) { }

        public override void Register(Hero owner)
        {
            Events.Handlers.Hero.Attacking += OnAttack;

            RegisterOwner(owner);
        }

        public override void Unregister(Hero owner)
        {
            Events.Handlers.Hero.Attacking -= OnAttack;
        }

        private void OnAttack(HeroAttackingEventArgs ev)
        {
            if (ev.Target != Owner) return;

            ev.Damage -= Values[$"damage_block_{Owner.HeroClassType.ToString().ToLower()}"][Level];
        }

        public override Ability Create()
        {
            return new DamageBlock();
        }
    }
}
