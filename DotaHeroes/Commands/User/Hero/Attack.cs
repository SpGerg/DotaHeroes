using DotaHeroes.API.Effects;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Events.EventArgs.Hero;
using DotaHeroes.API.Features.Objects;
using Exiled.API.Features.Toys;
using MEC;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DotaHeroes.Commands.User.Hero
{
    public class Attack : HeroCommandBase
    {
        public override string Command => "attack";

        public override string Description => "Hero attack";

        protected override bool Execute(API.Features.Hero hero, ArraySegment<string> arguments, out string response)
        {
            if (!API.Features.Utils.GetHeroFromPlayerEyeDirection(hero, (float)hero.HeroStatistics.Attack.AttackRange, out response, out API.Features.Hero target))
            {
                return false;
            }

            if (hero.TryGetEffect<Disarm>(out _))
            {
                response = "You are disarmed";
                return true;
            }

            var heroAttacking = new HeroAttackingEventArgs(hero, target, hero.HeroStatistics.Attack.FullDamage, DamageType.Physical, true);
            API.Events.Handlers.Hero.Attacking.InvokeSafely(heroAttacking);

            if (!heroAttacking.IsAllowed)
            {
                response = string.Empty;
                return true;
            }

            var isHit = target.HeroStatistics.Evasion.IsCanHit(hero.HeroStatistics.Evasion.AccuracyModifier);

            Timing.RunCoroutine(SoundCoroutine(hero, isHit));

            if (isHit)
            {
                if (hero.HeroClassType == HeroClassType.Ranged)
                {
                    Primitive primitive = Primitive.Create(hero.Player.Transform.position, hero.Player.Transform.rotation.eulerAngles, new Vector3(-0.2f, -0.2f, -0.2f), true);
                    primitive.Type = PrimitiveType.Cube;
                    var projectileObject = primitive.AdminToyBase.gameObject.AddComponent<ProjectileObject>();
                    projectileObject.Initialize(
                        hero,
                        target,
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
            }

            var heroAttacked = new HeroAttackedEventArgs(hero, target, hero.HeroStatistics.Attack.FullDamage, DamageType.Physical);
            API.Events.Handlers.Hero.Attacked.InvokeSafely(heroAttacked);

            response = "You attack " + target.HeroName;
            return true;
        }

        private IEnumerator<float> SoundCoroutine(API.Features.Hero hero, bool isHit)
        {
            API.Features.Audio.Play(hero.Player.Position, Plugin.Instance.SoundsPath + $"\\{hero.Slug}\\attack\\" + $"\\whoosh{UnityEngine.Random.Range(0, 4)}.ogg");
            yield return Timing.WaitForSeconds(0.5f);
            if (isHit) API.Features.Audio.Play(hero.Player.Position, Plugin.Instance.SoundsPath + $"\\{hero.Slug}\\attack\\" + $"\\attack{UnityEngine.Random.Range(0, 3)}.ogg");
        }
    }
}
