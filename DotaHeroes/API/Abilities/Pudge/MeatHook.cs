using CommandSystem;
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
    public class MeatHook : ActiveAbility, ICastRange, ICost, IValues
    {
        public override string Name => "Meat hook";

        public override string Description => "";

        public override string Lore => throw new NotImplementedException();

        public override AbilityType AbilityType => AbilityType.Active;

        public override TargetType TargetType => TargetType.ToPoint;

        public IReadOnlyDictionary<string, List<float>> Values => new Dictionary<string, List<float>>()
        {
            { "damage", new List<float> { 120, 180, 210, 240 } },
            { "mana_cost", new List<float> { 120, 130, 140, 150 } },
            { "cooldown", new List<float> { 12, 11, 9, 8 } },
            { "cast_range", new List<float> { 40, 60, 80, 100 } },
        };

        public override Cooldown Cooldown => new Cooldown((int)Values["cooldown"][Level]);

        public int Range { get; set; } = 1200;

        public int ManaCost { get; set; } = 0;

        public int HealthCost { get; set; } = -1;

        public override int MaxLevel => 4;

        public MeatHook() : base()
        {
            
        }

        public MeatHook(Player owner) : base(owner)
        {
            ManaCost = (int)Values["mana_cost"][0];
        }

        public override bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!base.Execute(arguments, sender, out response, true))
            {
                return false;
            }

            Primitive primitive = Primitive.Create(Owner.Position, Owner.Rotation.eulerAngles, Vector3.one, true);
            primitive.Type = PrimitiveType.Cube;
            var meatHookObject = primitive.AdminToyBase.gameObject.AddComponent<MeatHookObject>();
            meatHookObject.Initialization(
                Owner.GameObject.GetComponent<HeroController>(),
                Features.Utils.GetTargetPositionFromMouse(Owner.Transform.position, Owner.CameraTransform.forward, (int)Values["cast_range"][Level]),
                (int)Values["cast_range"][Level],
                20,
                (int)Values["damage"][Level],
                DamageType.Pure);
            var collider = primitive.AdminToyBase.gameObject.AddComponent<BoxCollider>();
            collider.isTrigger = true;
            primitive.Spawn();

            return true;
        }
    }
}
