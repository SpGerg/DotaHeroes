using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Interfaces;
using MEC;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DotaHeroes.API.Abilities.Morphling
{
    public class Waveform : ActiveAbility, ILevelValues
    {
        public override string Name => "Waveform";

        public override string Slug => "waveform";

        public override string Description => "Waveform";

        public override string Lore => "Waveform";

        public override AbilityType AbilityType => AbilityType.Active;

        public override TargetType TargetType => TargetType.ToPoint;

        public Dictionary<string, List<double>> Values { get; } = Plugin.Instance.Config.Abilites["waveform"].Values;

        public int MaxLevel { get; set; } = 4;

        public int MinLevel { get; set; } = 0;

        public IReadOnlyList<int> HeroLevelToLevelUp => Features.Utils.EmptyLevelsList;

        public Waveform(Hero hero) : base(hero) { }

        protected override bool Execute(ArraySegment<string> arguments, out string response)
        {
            Timing.RunCoroutine(MoveCoroutine());

            response = "Waveform was activated.";
            return true;
        }

        public override Ability Create(Hero hero)
        {
            return new Waveform(hero);
        }

        private IEnumerator<float> MoveCoroutine()
        {
            //700 -> 14
            var range = (int)(Values["range"][Level] / 48);
            var target = Features.Utils.GetTargetPositionFromMouse(Owner.Player.Position, Owner.Player.CameraTransform.forward, range);

            for (int i = 0;i < range;i++)
            {
                Owner.Player.Position += Vector3.MoveTowards(Owner.Player.Position, target, 2 * Time.deltaTime);

                yield return Timing.WaitForSeconds(0.05f);
            }
        }
    }
}
