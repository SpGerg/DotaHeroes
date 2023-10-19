using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Interfaces;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace DotaHeroes.API.Abilities.SpiritBreaker
{
    public class NetherStrike : ActiveAbility, ILevelValues
    {
        public override string Name => "Nether strike";

        public override string Slug => "nether_strike";

        public override string Description => "Nether strike";

        public override string Lore => "Nether strike";

        public override AbilityType AbilityType => AbilityType.Active;

        public override TargetType TargetType => TargetType.ToEnemy;

        public Dictionary<string, List<decimal>> Values { get; } = Plugin.Instance.Config.Abilites["nether_strike"].Values;

        public int MaxLevel { get; set; } = 3;
        public int MinLevel { get; set; } = 0;

        public IReadOnlyList<int> HeroLevelToLevelUp { get; } = Features.Utils.DefaultLevelsUltimateList;

        public static string SoundsPath = Plugin.Instance.SoundsPath + "\\spirit_breaker\\nether_strike";

        protected override bool Execute(Hero hero, ArraySegment<string> arguments, out string response)
        {
            if (!Features.Utils.GetHeroFromPlayerEyeDirection(hero, 8, out response, out Hero target))
            {
                return false;
            }

            Timing.RunCoroutine(NetherStrikeCoroutine(hero, target));

            hero.HeroStateType = HeroStateType.Casting;

            Audio.Play(hero.Player.Position, SoundsPath + "\\precast.ogg", 75f, false, hero.Player);

            response = "Your target is " + target.HeroName;
            return true;
        }

        private IEnumerator<float> NetherStrikeCoroutine(Hero hero, Hero target)
        {
            yield return Timing.WaitForSeconds(1);

            if (IsStop) yield break;

            hero.HeroStateType = HeroStateType.None;

            Audio.Play(hero.Player.Position, SoundsPath + "\\cast.ogg", 75f, false, hero.Player);

            hero.Player.Position = target.Player.Position;
            hero.Player.Rotation = target.Player.Rotation;
            hero.Player.CameraTransform.rotation = target.Player.CameraTransform.rotation;

            //target.Player.Position = -Vector3.MoveTowards(target.Player.Position, hero.Player.Position, 2 * Time.deltaTime);

            target.TakeDamage(hero, Values["damage"][Level], DamageType.Magical);

            var bash = hero.Abilities.FirstOrDefault(ability => ability is GreaterBash) as GreaterBash;

            if (bash == default) yield break;

            bash.Bash(target, hero);
        }
    }
}
