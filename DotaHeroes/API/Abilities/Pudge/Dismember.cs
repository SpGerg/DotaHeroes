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
    public class Dismember : ActiveAbility, ILevelValues
    {
        public override string Name => "Dismember";

        public override string Description => "Eating lol";

        public override string Lore => "Eating";

        public override AbilityType AbilityType => AbilityType.Active;

        public override TargetType TargetType => TargetType.ToFriendAndEnemy;

        public Dictionary<string, List<float>> Values => Plugin.Instance.Config.Abilites["dismember"].Values;

        public int MaxLevel { get; set; } = 3;

        public int MinLevel { get; set; } = 0;

        public const float Duration = 3.5f;

        public IReadOnlyList<int> HeroLevelToLevelUp { get; set; } = new List<int>()
        {
            6,
            12,
            18
        };

        public Dismember() : base() { }

        public override bool Execute(Hero hero, ArraySegment<string> arguments, out string response)
        {
            var player = hero.Player;

            if (!Features.Utils.GetHeroFromPlayerEyeDirection(player, 5, out response, out Hero target))
            {
                return false;
            }

            var duration = target.HeroStatistics.Resistance.GetEffectDuration(Duration);

            player.EnableEffect<Ensnared>(Duration);
            target.EnableEffect(new Stun(target)
            {
                Duration = duration
            });


            response = "You eating " + target.Player.Nickname;

            Timing.RunCoroutine(DamageCoroutine(hero, target, (decimal)Values["damage"][Level], DamageType.Magical, duration));

            return true;
        }

        private IEnumerator<float> DamageCoroutine(Hero hero, Hero target, decimal damage, DamageType damageType, float duration)
        {
            decimal times = (decimal)(duration / 0.1f);
            var damageOverTime = new DamageOverTime(target, damage / times, damageType, (int)times, 0.1f, hero);
            damageOverTime.Run();

            for (int i = 0; i < times; i++)
            {
                if (IsStop)
                {
                    hero.Player.DisableEffect<Ensnared>();
                    target.DisableEffect<Stun>();

                    damageOverTime.IsEnabled = false;

                    yield break;
                }

                target.Player.Position = Vector3.MoveTowards(target.Player.Position, hero.Player.Position, 2 * Time.deltaTime);

                yield return Timing.WaitForSeconds(0.1f);
            }

            hero.Player.DisableEffect<Ensnared>();
            target.DisableEffect<Stun>();
        }
    }
}
