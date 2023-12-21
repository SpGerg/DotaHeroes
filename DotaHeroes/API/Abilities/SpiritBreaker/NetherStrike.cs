using DotaHeroes.API.Abilities.Items;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Interfaces;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public Dictionary<string, List<double>> Values { get; } = Plugin.Instance.Config.Abilites["nether_strike"].Values;

        public int MaxLevel { get; set; } = 3;
        public int MinLevel { get; set; } = 0;

        public IReadOnlyList<int> HeroLevelToLevelUp { get; } = Features.Utils.DefaultLevelsUltimateList;

        public static string SoundsPath = Plugin.Instance.SoundsPath + "\\spirit_breaker\\nether_strike";

        public NetherStrike(Hero hero) : base(hero) { }

        protected override bool Execute(ArraySegment<string> arguments, out string response)
        {
            if (!Features.Utils.GetHeroFromPlayerEyeDirection(Owner, 8, out response, out Hero target))
            {
                return false;
            }

            Timing.RunCoroutine(NetherStrikeCoroutine(target));

            Owner.HeroStateType = HeroStateType.Casting;

            Audio.Play(Owner.Player.Position, SoundsPath + "\\precast.ogg", 75f, false, Owner.Player);

            response = "Your target is " + target.HeroName;
            return true;
        }

        private IEnumerator<float> NetherStrikeCoroutine(Hero target)
        {
            yield return Timing.WaitForSeconds(1);

            if (IsStop) yield break;

            Owner.HeroStateType = HeroStateType.None;

            Audio.Play(Owner.Player.Position, SoundsPath + "\\cast.ogg", 75f, false, Owner.Player);

            Owner.Player.Position = target.Player.Position;
            Owner.Player.Rotation = target.Player.Rotation;
            Owner.Player.CameraTransform.rotation = target.Player.CameraTransform.rotation;

            //target.Player.Position = -Vector3.MoveTowards(target.Player.Position, hero.Player.Position, 2 * Time.deltaTime);

            target.TakeDamage(Owner, Values["damage"][Level], DamageType.Magical);

            var bash = Owner.Abilities.FirstOrDefault(ability => ability is GreaterBash) as GreaterBash;

            if (bash == default) yield break;

            bash.Bash(target, Owner);
        }

        public override Ability Create(Hero hero)
        {
            return new NetherStrike(hero);
        }
    }
}
