using DotaHeroes.API.Features.Components;
using DotaHeroes.API.Features.Objects;
using Exiled.API.Features.Toys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

namespace DotaHeroes.Commands.Hero
{
    public class Attack : HeroCommandBase
    {
        public override string Command => "attack";

        public override string[] Aliases => Array.Empty<string>();

        public override string Description => "Hero attack";

        protected override bool Execute(API.Features.Hero hero, ArraySegment<string> arguments, out string response)
        {
            RaycastHit hit;

            var player = hero.Player;

            if (!Physics.Raycast(hero.Player.Transform.position, hero.Player.Transform.forward, out hit, hero.HeroStatistics.Attack.AttackRange))
            {
                response = string.Empty;

                return true;
            }

            if (!hit.collider.TryGetComponent(out HeroController target))
            {
                response = string.Empty;
                return true;
            }

            Primitive primitive = Primitive.Create(hero.Player.Transform.position, hero.Player.Transform.rotation.eulerAngles, Vector3.one, true);
            primitive.Color = new Color(199, 139, 0, 128);
            primitive.Type = PrimitiveType.Cube;
            var meatHookObject = primitive.AdminToyBase.gameObject.AddComponent<ProjectileObject>();
            meatHookObject.Initialization(
                hero.Player.GameObject.GetComponent<HeroController>(),
                hero.HeroStatistics.Attack.BaseAttackDamage + hero.HeroStatistics.Attack.MoreAttackDamage,
                API.Enums.DamageType.Physical,
                hero.ProjectileSpeed);
            var collider = primitive.AdminToyBase.gameObject.AddComponent<BoxCollider>();
            collider.isTrigger = true;
            primitive.Spawn();

            response = "Attack you " + target.Hero.HeroName;
            return true;
        }
    }
}
