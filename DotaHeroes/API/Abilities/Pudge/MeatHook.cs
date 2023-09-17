using CommandSystem;
using CustomPlayerEffects;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Features.Components;
using DotaHeroes.API.Features.Objects;
using DotaHeroes.API.Interfaces;
using Exiled.API.Features;
using Exiled.API.Features.Toys;
using RemoteAdmin;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DotaHeroes.API.Abilities.Pudge
{
    [CommandHandler(typeof(ClientCommandHandler))]
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class MeatHook : ActiveAbility, ICastRange, ICost, IValues
    {
        public override string Name => "Meat hook";

        public override string Command { get; set; } = "meathook";

        public override string Description => string.Empty;

        public override string Lore => "Meat hook";

        public override AbilityType AbilityType => AbilityType.Active;

        public override TargetType TargetType => TargetType.ToPoint;

        public IReadOnlyDictionary<string, List<float>> Values => new Dictionary<string, List<float>>()
        {
            { "damage", new List<float> { 120, 180, 210, 240 } },
            { "mana_cost", new List<float> { 120, 130, 140, 150 } },
            { "cooldown", new List<float> { 12, 11, 9, 8 } },
            { "cast_range", new List<float> { 40, 60, 80, 100 } },
        };

        public int Range { get; set; } = 1200;

        public int ManaCost { get; set; } = 0;

        public int HealthCost { get; set; } = -1;

        public override int MaxLevel => 4;

        public MeatHook() : base()
        {
            ManaCost = (int)Values["mana_cost"][0];
        }

        public override bool Execute(Hero hero, ArraySegment<string> arguments, out string response)
        {
            var player = hero.Player;

            Primitive primitive = Primitive.Create(player.Position, player.Rotation.eulerAngles, Vector3.one, false);
            primitive.Type = PrimitiveType.Cube;
            var meatHookObject = primitive.AdminToyBase.gameObject.AddComponent<MeatHookObject>();
            meatHookObject.Initialization(
                player.GameObject.GetComponent<HeroController>(),
                Features.Utils.GetTargetPositionFromMouse(player.Transform.position, player.CameraTransform.forward, (int)Values["cast_range"][Level]),
                (int)Values["cast_range"][Level],
                35,
                (int)Values["damage"][Level],
                DamageType.Pure);
            var rigidbody = primitive.AdminToyBase.gameObject.AddComponent<Rigidbody>();
            rigidbody.isKinematic = true;
            var collider = primitive.AdminToyBase.gameObject.AddComponent<BoxCollider>();
            collider.isTrigger = true;
            player.EnableEffect<Ensnared>();
            primitive.Spawn();

            response = "Hook him!";
            return true;
        }
    }
}
