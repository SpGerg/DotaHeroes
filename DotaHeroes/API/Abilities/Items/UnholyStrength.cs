using DotaHeroes.API.Effects.Items;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Interfaces;
using Exiled.API.Features;
using MEC;
using NorthwoodLib.Pools;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHeroes.API.Abilities.Items
{
    public class UnholyStrength : ToggleAbility, ILevelValues
    {
        public override string Name => "Unholy strength";

        public override string Slug => "unholy_strength";

        public override string Description => "Unholy strength";

        public override string Lore => "Unholy strength";

        public override AbilityType AbilityType => AbilityType.Toggle;

        public override TargetType TargetType => TargetType.None;

        public Dictionary<string, List<decimal>> Values { get; } = Plugin.Instance.Config.Abilites["unholy_strength"].Values;

        public int MaxLevel { get; set; } = 1;

        public int MinLevel { get; set; } = 1;

        public IReadOnlyList<int> HeroLevelToLevelUp => throw new NotImplementedException();

        public UnholyStrength() : base() { }

        public UnholyStrength(Hero hero) : base(hero) { }

        public override void LevelUp() { }

        public override bool Activate(ArraySegment<string> arguments, out string response)
        {
            var unholyStrength = new Effects.Items.UnholyStrength(Owner);
            unholyStrength.ExtraAttackDamage = Values["extra_attack_damage"][Level];
            unholyStrength.Strength = Values["strength"][Level];
            unholyStrength.Armor = Values["armor"][Level];

            Owner.EnableEffect(unholyStrength);

            response = "Armlet is enabled";
            return true;
        }

        public override bool Deactivate(ArraySegment<string> arguments, out string response)
        {
            Owner.DisableEffect<Effects.Items.UnholyStrength>();

            response = "Armlet is disabled";
            return true;
        }

        public override Ability Create(Hero hero)
        {
            return new UnholyStrength(hero);
        }
    }
}
