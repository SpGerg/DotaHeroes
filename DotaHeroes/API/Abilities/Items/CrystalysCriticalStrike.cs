﻿using DotaHeroes.API.Enums;
using DotaHeroes.API.Events.EventArgs.Hero;
using DotaHeroes.API.Features;
using DotaHeroes.API.Interfaces;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Abilities.Items
{
    public class CrystalysCriticalStrike : PassiveAbility, ILevelValues
    {
        public override string Name => "Crystalys critical strike";

        public override string Slug => "crystalys_critical_strike";

        public override string Description => "Crystalys critical strike";

        public override string Lore => "Crystalys critical strike";

        public Dictionary<string, List<decimal>> Values { get; } = Plugin.Instance.Config.Abilites["crystalys_critical_strike"].Values;

        public int MaxLevel { get; set; } = 1;

        public int MinLevel { get; set; } = 1;

        public IReadOnlyList<int> HeroLevelToLevelUp => throw new NotImplementedException();

        public override AbilityType AbilityType => AbilityType.Passive;

        public override TargetType TargetType => TargetType.None;

        public CrystalysCriticalStrike() : base() { }

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
            if (ev.Hero != Owner) return;

            if (UnityEngine.Random.Range(0, 100) < Values["chance"][Level]) return;

            ev.Damage += Features.Utils.GetPercentOfValue(ev.Damage, 100 - Values["critical_damage"][Level]);
            ev.Hero.Player.SendConsoleMessage("Critical damage: " + ev.Damage, "default");
        }

        public override Ability Create()
        {
            return new CrystalysCriticalStrike();
        }
    }
}
