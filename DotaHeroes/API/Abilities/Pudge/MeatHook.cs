using CustomPlayerEffects;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Features.Objects;
using DotaHeroes.API.Interfaces;
using Exiled.API.Features.Toys;
using MEC;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DotaHeroes.API.Abilities.Pudge
{
    public class MeatHook : ActiveAbility, ICost, ILevelValues
    {
        public override string Name => "Meat hook";

        public override string Slug => "meat_hook";

        public override string Description => string.Empty;

        public override string Lore => "Meat hook";

        public override AbilityType AbilityType => AbilityType.Active;

        public override TargetType TargetType => TargetType.ToPoint;

        public Dictionary<string, List<double>> Values { get; } = Plugin.Instance.Config.Abilites["meat_hook"].Values;

        public int MaxLevel { get; set; } = 4;

        public int MinLevel { get; set; } = 0;

        public IReadOnlyList<int> HeroLevelToLevelUp { get; set; } = Features.Utils.EmptyLevelsList;

        public int ManaCost { get; set; } = 0;

        public int HealthCost { get; set; } = -1;

        public static string SoundsPath = Plugin.Instance.SoundsPath + "\\pudge\\meat_hook";

        public MeatHook(Hero hero) : base(hero)
        {
            ManaCost = (int)Values["mana_cost"][0];
        }

        protected override bool Execute(ArraySegment<string> arguments, out string response)
        {
            var player = Owner.Player;

            var sound = Features.Audio.Play(player.Position, $"{SoundsPath}\\moving_to_target.ogg");

            Primitive primitive = Primitive.Create(player.Position, player.Rotation.eulerAngles, new Vector3(-0.5f, -0.5f, -0.5f), false);
            primitive.Type = PrimitiveType.Cube;
            primitive.MovementSmoothing = 60;
            primitive.Collidable = false;
            var meatHookObject = primitive.AdminToyBase.gameObject.AddComponent<MeatHookObject>();
            meatHookObject.Initialize(
                Owner,
                Features.Utils.GetTargetPositionFromMouse(player.Transform.position, player.CameraTransform.forward, (int)Values["cast_range"][Level]),
                (int)Values["cast_range"][Level],
                25,
                (int)Values["damage"][Level],
                DamageType.Pure,
                sound);
            var rigidbody = primitive.AdminToyBase.gameObject.AddComponent<Rigidbody>();
            rigidbody.isKinematic = true;
            player.EnableEffect<Ensnared>();
            primitive.Spawn();

            Timing.RunCoroutine(WaitForHookEndCoroutine(primitive, meatHookObject));

            response = "Hook him!";
            return true;
        }

        private IEnumerator<float> WaitForHookEndCoroutine(Primitive primitive, MeatHookObject meatHookObject)
        {
            while (!meatHookObject.IsEnded)
            {
                yield return Timing.WaitForOneFrame;
            }

            yield return Timing.WaitForSeconds(0.35f);

            primitive.Destroy();
        }

        public override Ability Create(Hero hero)
        {
            return new MeatHook(hero);
        }
    }
}
