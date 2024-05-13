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

        public override TargetType TargetType => TargetType.ToEnemy | TargetType.ToFriend;

        public Dictionary<string, List<double>> Values { get; } = Plugin.Instance.Config.Abilites["dismember"].Values;

        public int MaxLevel { get; set; } = 3;

        public int MinLevel { get; set; } = 0;

        public const double Duration = 3.5;

        public IReadOnlyList<int> HeroLevelToLevelUp { get; set; } = Features.Utils.DefaultLevelsUltimateList;

        public static string SoundsPath = Plugin.Instance.SoundsPath + "\\pudge\\dismember";

        public Dismember(Hero hero) : base(hero) { }

        protected override bool Execute(ArraySegment<string> arguments, out string response)
        {
            if (!Features.Utils.GetHeroFromPlayerEyeDirection(Owner, 5, out response, out Hero target))
            {
                return false;
            }

            var duration = target.HeroStatistics.Resistance.GetEffectDuration(Duration);

            Owner.HeroStateType = HeroStateType.Casting;

            Owner.Player.EnableEffect<Ensnared>((float)Duration);
            target.EnableEffect(new Stun(target)
            {
                Duration = (float)duration
            });


            response = "You eating " + target.Player.Nickname;

            Timing.RunCoroutine(DamageCoroutine(target, Values["damage"][Level], DamageType.Magical, (float)duration));
            Timing.RunCoroutine(SoundCoroutine(Owner));

            return true;
        }

        private IEnumerator<float> DamageCoroutine(Hero target, double damage, DamageType damageType, float duration)
        {
            double times = (double)(duration / 0.1f);
            var damageOverTime = new DamageOverTime(target, Math.Round((damage / times) * 2), damageType, (int)times, 0.1f, Owner);
            damageOverTime.Run();

            for (int i = 0; i < times; i++)
            {
                if (IsStop)
                {
                    Owner.Player.DisableEffect<Ensnared>();
                    target.DisableEffect<Stun>();

                    damageOverTime.IsEnabled = false;

                    yield break;
                }

                target.Player.Position = Vector3.MoveTowards(target.Player.Position, Owner.Player.Position, 2 * Time.deltaTime);

                yield return Timing.WaitForSeconds(0.1f);
            }

            Owner.Player.DisableEffect<Ensnared>();
            target.DisableEffect<Stun>();
            Owner.HeroStateType = HeroStateType.None;
        }

        private IEnumerator<float> SoundCoroutine(Hero hero)
        {
            for (int i = 0; i < 3; i++)
            {
                Features.Audio.Play(Owner.Player.Position, SoundsPath + $"\\blood{i}.ogg");
                Features.Audio.Play(Owner.Player.Position, SoundsPath + $"\\swing{i}.ogg");

                yield return Timing.WaitForSeconds(0.5f);
            }

            yield return Timing.WaitForSeconds(0.5f);

            for (int i = 0; i < 2; i++)
            {
                Features.Audio.Play(Owner.Player.Position, SoundsPath + $"\\blood{i}.ogg");
                Features.Audio.Play(Owner.Player.Position, SoundsPath + $"\\swing{i}.ogg");

                yield return Timing.WaitForSeconds(0.5f);
            }
        }

        public override Ability Create(Hero hero)
        {
            return new Dismember(hero);
        }
    }
}
