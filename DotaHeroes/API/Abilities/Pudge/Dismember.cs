﻿using CommandSystem;
using CustomPlayerEffects;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Features.Components;
using DotaHeroes.API.Interfaces;
using Exiled.API.Features;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DotaHeroes.API.Abilities
{
    public class Dismember : ActiveAbility, ICastRange, IValues
    {
        public override string Name => "Dismember";

        public override string Description => "Eating lol";

        public override string Lore => "Eating";

        public override AbilityType AbilityType => AbilityType.Active;

        public override TargetType TargetType => TargetType.ToFriendAndEnemy;

        public override int MaxLevel => 4;

        public int Range { get; set; } = 1;

        public IReadOnlyDictionary<string, List<float>> Values => new Dictionary<string, List<float>>()
        {
            { "damage", new List<float>() { 120, 150, 180, 210 } },
            { "cooldown", new List<float>() { 30, 25, 20, 15 } },
        };

        public override bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!base.Execute(arguments, sender, out response, false))
            {
                return false;
            }

            RaycastHit hit;

            if (!Physics.Raycast(Owner.Transform.position, Owner.Transform.forward, out hit, Range))
            {
                return false;
            }

            var target = Player.Get(hit.collider.gameObject);

            if (target == null)
            {
                response = "Target not found.";
                return true;
            }

            var hero = API.GetHeroOrDefault(target.UserId);

            if (hero == default)
            {
                response = "Target is not hero.";
                return true;
            }

            hero.Player.EnableEffect<Ensnared>();
            target.EnableEffect<Ensnared>();

            response = "You eating " + target.Nickname;

            var damage = Values["damage"][Level];

            Timing.RunCoroutine(DamageCoroutine(damage, DamageType.Magical, hero));

            return true;
        }

        private IEnumerator<float> DamageCoroutine(float damage, DamageType damageType, Hero target)
        {
            var player = target.Player;

            for (int i = 0; i < (damage / 8); i += (int)damage / 8)
            {
                if (IsStop)
                {
                    player.EnableEffect<Ensnared>();

                    yield break;
                }

                player.DisableEffect<Ensnared>();
                target.HeroController.TakeDamage(i, DamageType.Magical);

                yield return Timing.WaitForSeconds(0.1f);
            }
        }
    }
}
