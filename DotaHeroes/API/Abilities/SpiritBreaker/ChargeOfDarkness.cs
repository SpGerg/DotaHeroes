using CommandSystem;
using DotaHeroes.API.Effects.SpiritBreaker;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Interfaces;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DotaHeroes.API.Abilities.SpiritBreaker
{
    [CommandHandler(typeof(ClientCommandHandler))]
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class ChargeOfDarkness : ActiveAbility, ILevelValues
    {
        public override string Name => "Charge of darkness";

        public override string Slug => "charge_of_darkness";

        public override string Description => "Charge of darkness";

        public override string Lore => "Charge of darkness";

        public override AbilityType AbilityType => AbilityType.Active;

        public override TargetType TargetType => TargetType.ToEnemy;

        public Dictionary<string, List<decimal>> Values { get; } = Plugin.Instance.Config.Abilites["charge_of_darkness"].Values;

        public int MaxLevel { get; set; } = 4;

        public int MinLevel { get; set; } = 0;

        public IReadOnlyList<int> HeroLevelToLevelUp { get; } = Features.Utils.EmptyLevelsList;

        public static string SoundsPath = Plugin.Instance.SoundsPath + "\\spirit_breaker\\charge_of_darkness";

        protected override bool Execute(Hero hero, ArraySegment<string> arguments, out string response)
        {
            if (!Features.Utils.GetHeroFromPlayerEyeDirection(hero, Mathf.Infinity, out response, out Hero target))
            {
                return false;
            }

            var effect = new ChargeOfDarknessSpeed(hero);
            effect.ExtraSpeed = (sbyte)Values["extra_speed"][Level];

            hero.EnableEffect(effect);
            hero.HeroStateType = HeroStateType.Casting;
            
            Timing.RunCoroutine(RunningCoroutine(hero, target));

            foreach (var _hero in API.GetHeroes().Values)
            {
                Audio.Play(_hero.Player.Position, SoundsPath + "\\start.ogg", 100f, false, _hero.Player);
            }

            response = $"Your target is {target.HeroName}";
            return true;
        }

        public override void Stop(Hero hero)
        {
            hero.DisableEffect<ChargeOfDarknessSpeed>();
            hero.HeroStateType = HeroStateType.None;

            base.Stop(hero);
        }

        private IEnumerator<float> RunningCoroutine(Hero hero, Hero target)
        {
            while (Vector3.Distance(hero.Player.Position, target.Player.Position) > 0.5f)
            {
                hero.Player.Position = Vector3.MoveTowards(hero.Player.Position, target.Player.Position, hero.HeroStatistics.Speed.Speed * Time.deltaTime);

                yield return Timing.WaitForOneFrame;

                if (target.Player.IsConnected && target.IsHeroDead)
                {
                    yield break;
                }
            }

            Stop(hero);

            var bash = hero.Abilities.FirstOrDefault(ability => ability is GreaterBash) as GreaterBash;

            if (bash == default) yield break;

            bash.Bash(target, hero);
        }
    }
}
