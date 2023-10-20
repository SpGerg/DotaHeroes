using CustomPlayerEffects;
using DotaHeroes.API.Effects;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Interfaces;
using MEC;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DotaHeroes.API.Abilities.Pudge
{
    public class Dismember : ActiveAbility, ILevelValues
    {
        public override string Name => "Dismember";

        public override string Slug => "dismember";

        public override string Description => "Eating lol";

        public override string Lore => "Eating";

        public override AbilityType AbilityType => AbilityType.Active;

        public override TargetType TargetType => TargetType.ToFriendAndEnemy;

        public Dictionary<string, List<decimal>> Values { get; } = Plugin.Instance.Config.Abilites["dismember"].Values;

        public int MaxLevel { get; set; } = 3;

        public int MinLevel { get; set; } = 0;

        public const decimal Duration = 3.5m;

        public IReadOnlyList<int> HeroLevelToLevelUp { get; set; } = Features.Utils.DefaultLevelsUltimateList;

        public static string SoundsPath = Plugin.Instance.SoundsPath + "\\pudge\\dismember";

        public Dismember() : base() { }

        protected override bool Execute(Hero hero, ArraySegment<string> arguments, out string response)
        {
            var player = hero.Player;

            if (!Features.Utils.GetHeroFromPlayerEyeDirection(hero, 5, out response, out Hero target))
            {
                return false;
            }

            var duration = target.HeroStatistics.Resistance.GetEffectDuration(Duration);

            hero.HeroStateType = HeroStateType.Casting;

            player.EnableEffect<Ensnared>((float)Duration);
            target.EnableEffect(new Stun(target)
            {
                Duration = (float)duration
            });


            response = "You eating " + target.Player.Nickname;

            Timing.RunCoroutine(DamageCoroutine(hero, target, Values["damage"][Level], DamageType.Magical, (float)duration));
            Timing.RunCoroutine(SoundCoroutine(hero));

            return true;
        }

        private IEnumerator<float> DamageCoroutine(Hero hero, Hero target, decimal damage, DamageType damageType, float duration)
        {
            decimal times = (decimal)(duration / 0.1f);
            var damageOverTime = new DamageOverTime(target, Math.Round((damage / times) * 2), damageType, (int)times, 0.1f, hero);
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
            hero.HeroStateType = HeroStateType.None;
        }

        private IEnumerator<float> SoundCoroutine(Hero hero)
        {
            for (int i = 0;i < 3;i++)
            {
                Audio.Play(hero.Player.Position, SoundsPath + $"\\blood{i}.ogg");
                Audio.Play(hero.Player.Position, SoundsPath + $"\\swing{i}.ogg");

                yield return Timing.WaitForSeconds(0.5f);
            }

            yield return Timing.WaitForSeconds(0.5f);

            for (int i = 0; i < 2; i++)
            {
                Audio.Play(hero.Player.Position, SoundsPath + $"\\blood{i}.ogg");
                Audio.Play(hero.Player.Position, SoundsPath + $"\\swing{i}.ogg");

                yield return Timing.WaitForSeconds(0.5f);
            }
        }
    }
}
