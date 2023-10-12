using CommandSystem;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Events.EventArgs.Hero;
using DotaHeroes.API.Events.Handlers;
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

namespace DotaHeroes.Commands.User.Hero
{
    public class Attack : HeroCommandBase
    {
        public override string Command => "attack";

        public override string Description => "Hero attack";

        protected override bool Execute(API.Features.Hero hero, ArraySegment<string> arguments, out string response)
        {
            RaycastHit hit;

            var player = hero.Player;

            if (!API.Features.Utils.GetHeroFromPlayerEyeDirection(player, (int)hero.HeroStatistics.Attack.AttackRange, out response, out API.Features.Hero target))
            {
                return false;
            }

            var heroAttacking = new HeroAttackingEventArgs(hero, target, hero.HeroStatistics.Attack.FullDamage, DamageType.Physical, true);
            API.Events.Handlers.Hero.Attacking.InvokeSafely(heroAttacking);

            if (!heroAttacking.IsAllowed)
            {
                response = string.Empty;
                return true;
            }

            if (hero.HeroClassType is HeroClassType.Ranged)
            {
                Primitive primitive = Primitive.Create(hero.Player.Transform.position, hero.Player.Transform.rotation.eulerAngles, Vector3.one, true);
                primitive.Color = new Color(199, 139, 0, 128);
                primitive.Type = PrimitiveType.Cube;
                var projectileObject = primitive.AdminToyBase.gameObject.AddComponent<ProjectileObject>();
                projectileObject.Initialize(
                    hero.Player.GameObject.GetComponent<HeroController>(),
                    hero.HeroStatistics.Attack.BaseAttackDamage + hero.HeroStatistics.Attack.ExtraAttackDamage,
                    DamageType.Physical,
                    hero.ProjectileSpeed);
                var collider = primitive.AdminToyBase.gameObject.AddComponent<BoxCollider>();
                collider.isTrigger = true;
                primitive.Spawn();
            }
            else
            {
                hero.Attack(target);
            }

            var heroAttacked = new HeroAttackedEventArgs(hero, target, hero.HeroStatistics.Attack.FullDamage, DamageType.Physical);
            API.Events.Handlers.Hero.Attacked.InvokeSafely(heroAttacked);

            response = "You attack " + target.HeroName;
            return true;
        }
    }
}
