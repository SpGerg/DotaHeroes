﻿using DotaHeroes.API.Abilities.Items;
using DotaHeroes.API.Effects.Items;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Auras.Items
{
    public class BucklerAura : Aura
    {
        public override string Name => "Buckler aura";

        public override string Slug => "buckler_aura";

        public override string Description => "Buckler aura";

        public override TargetType TargetType => TargetType.ToFriend;

        public override AbilityType AbilityType => AbilityType.None;

        public override float Radius { get; set; } = 5;

        public decimal Armor { get; set; } = Plugin.Instance.Config.Abilites["buckler_aura"].Values["extra_armor"][0];

        public BucklerAura() : base() { }

        public BucklerAura(Hero owner) : base(owner)
        {
            
        }

        public override void Added(Hero hero)
        {
            var buckler = new Effects.Items.Buckler(hero);
            buckler.Armor = Armor;

            hero.EnableEffect(buckler);

            base.Added(hero);
        }

        public override void Removed(Hero hero)
        {
            hero.DisableEffect<Effects.Items.Buckler>();

            base.Removed(hero);
        }

        public override Ability Create(Hero hero)
        {
            return new BucklerAura(hero);
        }
    }
}