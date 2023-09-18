﻿using CommandSystem;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Features.Components;
using DotaHeroes.API.Features.Objects;
using DotaHeroes.API.Interfaces;
using Exiled.API.Features;
using Exiled.API.Features.Toys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DotaHeroes.API.Abilities.Pudge
{
    [CommandHandler(typeof(ClientCommandHandler))]
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Rot : ToggleAbility, ICastRange, IValues
    {
        public override string Name => "rot";

        public override string Description => "A toxic cloud that deals intense damage and slows movement--harming not only enemy units but Pudge himself.";

        public override string Lore => "A foul odor precedes a toxic, choking gas, emanating from the Butcher's putrid, ever-swelling mass.";

        public override AbilityType AbilityType => AbilityType.Toggle;

        public override TargetType TargetType => TargetType.None;

        public IReadOnlyDictionary<string, List<float>> Values => new Dictionary<string, List<float>>()
        {
            { "damage", new List<float> { 40, 60, 80, 100 } },
            { "mana_cost", new List<float> { 0, 0, 0, 0 } },
            { "cast_range", new List<float> { 1.5f, 2, 4, 5 } },
        };

        public override int MaxLevel => 4;

        public int Range { get; set; } = 5;

        public override bool IsActive { get; set; }

        public Rot() : base()
        {
        }

        public override bool Execute(Hero hero, ArraySegment<string> arguments, out string response)
        {
            var player = hero.Player;

            if (!IsActive)
            {
                response = "Rot is disabled";

                return true;
            }

            Primitive primitive = Primitive.Create(player.Position, player.Rotation.eulerAngles, Vector3.one, true);
            primitive.MovementSmoothing = 60;
            primitive.Color = new Color(199, 139, 0, 64);
            primitive.Type = PrimitiveType.Cube;
            primitive.Collidable = false;
            var meatHookObject = primitive.AdminToyBase.gameObject.AddComponent<RotObject>();
            meatHookObject.Initialization(
                player.GameObject.GetComponent<HeroController>(),
                (int)Values["cast_range"][Level],
                (int)Values["damage"][Level],
                DamageType.Magical);
            var rigidbody = primitive.AdminToyBase.gameObject.AddComponent<Rigidbody>();
            rigidbody.isKinematic = true;
            primitive.Spawn();

            response = "Rot is enabled";

            return true;
        }
    }
}
