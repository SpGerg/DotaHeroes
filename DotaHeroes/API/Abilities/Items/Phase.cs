﻿using DotaHeroes.API.Effects.Items;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Interfaces;
using System;
using System.Collections.Generic;

namespace DotaHeroes.API.Abilities.Items
{
    public class Phase : ActiveAbility, ILevelValues
    {
        public Phase(Hero owner) : base(owner) { }

        public override string Name => "Phase";

        public override string Slug => "phase";

        public override string Description => "Phase";

        public override string Lore => "Phase";

        public override AbilityType AbilityType => AbilityType.Active;

        public override TargetType TargetType => TargetType.None;

        public Dictionary<string, List<double>> Values { get; } = Plugin.Instance.Config.Abilites["phase"].Values;

        public int MaxLevel { get; set; } = 1;

        public int MinLevel { get; set; } = 1;

        public IReadOnlyList<int> HeroLevelToLevelUp => throw new NotImplementedException();

        public override void LevelUp() { }

        protected override bool Execute(ArraySegment<string> arguments, out string response)
        {
            var phase = new PhaseSpeed(Owner);
            //lol
            phase.ExtraSpeed = (sbyte)Values["extra_speed_" + Owner.HeroClassType.ToString().ToLower()][Level];

            Owner.EnableEffect(phase, (float)Values["duration"][Level]);

            response = "Phase boots was used";
            return true;
        }

        public override Ability Create(Hero hero)
        {
            return new Phase(hero);
        }
    }
}
