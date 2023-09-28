using CommandSystem;
using CustomPlayerEffects;
using DotaHeroes.API.Effects;
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

namespace DotaHeroes.API.Abilities.Pudge
{
    [CommandHandler(typeof(ClientCommandHandler))]
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Dismember : ActiveAbility, IValues
    {
        public override string Name => "Dismember";

        public override string Command { get; set; } = "dismember";

        public override string Description => "Eating lol";

        public override string Lore => "Eating";

        public override AbilityType AbilityType => AbilityType.Active;

        public override TargetType TargetType => TargetType.ToFriendAndEnemy;

        public override int MaxLevel => 4;

        public const float Duration = 3.5f;

        public int Range { get; set; } = 10;

        public IReadOnlyDictionary<string, List<float>> Values => new Dictionary<string, List<float>>()
        {
            { "damage", new List<float>() { 120, 150, 180, 210 } },
            { "cooldown", new List<float>() { 30, 25, 20, 15 } },
        };

        public Dismember() : base() { }

        public override bool Execute(Hero hero, ArraySegment<string> arguments, out string response)
        {
            var player = hero.Player;

            RaycastHit hit;

            if (!Physics.Raycast(player.Transform.position, player.CameraTransform.forward, out hit, Range))
            {
                response = "Target not found";

                return false;
            }

            var target = Player.Get(hit.collider);

            if (target == null)
            {
                response = "Target is not player";

                return false;
            }

            if (!target.GameObject.TryGetComponent(out HeroController heroController))
            {
                response = "Target is not hero.";

                return false;
            }

            player.EnableEffect<Ensnared>();
            heroController.Hero.EnableEffect(new Stun(heroController.Hero)
            {
                Duration = Duration
            });

            response = "You eating " + heroController.Hero.Player.Nickname;

            Timing.RunCoroutine(DamageCoroutine(hero, heroController.Hero, Values["damage"][Level], DamageType.Magical));

            return true;
        }

        private IEnumerator<float> DamageCoroutine(Hero player, Hero target, float damage, DamageType damageType)
        {
            for (int i = 0; i < (damage / 8); i += (int)damage / 8)
            {
                if (IsStop)
                {
                    player.Player.DisableEffect<Ensnared>();
                    target.DisableEffect<Stun>();

                    yield break;
                }

                target.Player.Position = Vector3.MoveTowards(target.Player.Position, player.Player.Position, 2 * Time.deltaTime);

                target.TakeDamage(i, damageType);

                yield return Timing.WaitForSeconds(0.1f);
            }
        }
    }
}
