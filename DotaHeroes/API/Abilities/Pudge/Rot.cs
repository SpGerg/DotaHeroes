using CommandSystem;
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
    public class Rot : ToggleAbility, ICastRange, IValues
    {
        public override string Name => "Rot";

        public override string Description => "A toxic cloud that deals intense damage and slows movement--harming not only enemy units but Pudge himself.";

        public override string Lore => "A foul odor precedes a toxic, choking gas, emanating from the Butcher's putrid, ever-swelling mass.";

        public override AbilityType AbilityType => AbilityType.Toggle;

        public override TargetType TargetType => TargetType.None;

        public IReadOnlyDictionary<string, List<float>> Values => new Dictionary<string, List<float>>()
        {
            { "damage", new List<float> { 40, 60, 80, 100 } },
            { "mana_cost", new List<float> { 0, 0, 0, 0 } },
            { "cooldown", new List<float> { 0, 0, 0, 0 } },
            { "cast_range", new List<float> { 1.5f, 2, 4, 5 } },
        };

        public override int MaxLevel => 4;

        public int Range { get; set; } = 5;

        public override bool IsActive { get; set; }

        public Rot(Player owner) : base(owner)
        {
        }

        public override bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!base.Execute(arguments, sender, out response, false)) {
                return false;
            }

            if (!IsActive)
            {
                return true;
            }

            Primitive primitive = Primitive.Create(Owner.Position, Owner.Rotation.eulerAngles, Vector3.one, true);
            primitive.Color = new Color(199, 139, 0, 128);
            primitive.Type = PrimitiveType.Cube;
            var meatHookObject = primitive.AdminToyBase.gameObject.AddComponent<RotObject>();
            meatHookObject.Initialization(
                Owner.GameObject.GetComponent<HeroController>(),
                (int)Values["range"][Level],
                (int)Values["damage"][Level],
                DamageType.Magical);
            var collider = primitive.AdminToyBase.gameObject.AddComponent<BoxCollider>();
            collider.isTrigger = true;
            primitive.Spawn();

            return true;
        }
    }
}
