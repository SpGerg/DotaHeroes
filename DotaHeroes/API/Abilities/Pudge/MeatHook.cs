using CommandSystem;
using CustomPlayerEffects;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Features.Components;
using DotaHeroes.API.Features.Objects;
using DotaHeroes.API.Interfaces;
using Exiled.API.Features;
using Exiled.API.Features.Toys;
using MEC;
using RemoteAdmin;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DotaHeroes.API.Abilities.Pudge
{
    [CommandHandler(typeof(ClientCommandHandler))]
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class MeatHook : ActiveAbility, ICost, ILevelValues
    {
        public override string Name => "Meat hook";

        public override string Description => string.Empty;

        public override string Lore => "Meat hook";

        public override AbilityType AbilityType => AbilityType.Active;

        public override TargetType TargetType => TargetType.ToPoint;

        public Dictionary<string, List<float>> Values => Plugin.Instance.Config.Abilites["meathook"].Values;

        public int MaxLevel { get; set; } = 4;

        public int MinLevel { get; set; } = 0;

        public IReadOnlyList<int> HeroLevelToLevelUp { get; set; } = new List<int>();

        public int ManaCost { get; set; } = 0;

        public int HealthCost { get; set; } = -1;

        public MeatHook() : base()
        {
            ManaCost = (int)Values["mana_cost"][0];
        }

        public override bool Execute(Hero hero, ArraySegment<string> arguments, out string response)
        {
            var player = hero.Player;

            Primitive primitive = Primitive.Create(player.Position, player.Rotation.eulerAngles, new Vector3(-0.5f, -0.5f, -0.5f), false);
            primitive.Type = PrimitiveType.Cube;
            primitive.MovementSmoothing = 60;
            primitive.Collidable = false;
            var meatHookObject = primitive.AdminToyBase.gameObject.AddComponent<MeatHookObject>();
            meatHookObject.Initialize(
                hero,
                Features.Utils.GetTargetPositionFromMouse(player.Transform.position, player.CameraTransform.forward, (int)Values["cast_range"][Level]),
                (int)Values["cast_range"][Level],
                25,
                (int)Values["damage"][Level],
                DamageType.Pure);
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
    }
}
