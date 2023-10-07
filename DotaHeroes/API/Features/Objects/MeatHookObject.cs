using CustomPlayerEffects;
using DotaHeroes.API.Effects;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Features.Components;
using DotaHeroes.API.Interfaces;
using Exiled.API.Features;
using Exiled.API.Features.Toys;
using MEC;
using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DotaHeroes.API.Features.Objects
{
    public class MeatHookObject : NetworkBehaviour, IDamage
    {
        public Hero Owner { get; private set; }

        public Hero HeroTarget { get; private set; }

        public Vector3 Target { get; private set; }

        public int Range { get; set; }

        public float Step { get; private set; }

        public float Speed { get; private set; }

        public decimal Damage { get; set; }

        public DamageType DamageType { get; set; }

        private bool isMovingToTarget { get; set; } = true;

        private Vector3 usePosition { get; set; }

        public bool IsEnded { get; set; }

        private float moveLength { get; set; }

        private bool isDestroying { get; set; }

        public void Initialization(Hero owner, Vector3 target, int range, float speed, int damage, DamageType damageType)
        {
            Owner = owner;
            Target = target;
            Range = range;
            Speed = speed;
            Damage = damage;
            DamageType = damageType;

            usePosition = transform.position;
            Step = Speed * Time.deltaTime;

            Timing.CallDelayed(1f, () =>
            {
                isDestroying = true;
            });
        }

        public void Update()
        {
            if (!isMovingToTarget) return;

            foreach (var hero in API.GetHeroes().Values)
            {
                if (Vector3.Distance(Owner.Player.Position, hero.Player.Position) < 1)
                {
                    if (hero.Player.UserId == Owner.Player.UserId)
                    {
                        return;
                    }

                    isMovingToTarget = false;

                    HeroTarget = hero;
                    HeroTarget.Player.IsGodModeEnabled = true;
                    HeroTarget.EnableEffect<Stun>();

                    if (hero.SideType == Owner.SideType)
                    {
                        return;
                    }

                    hero.TakeDamage(Damage, DamageType);
                }
            }
        }

        public void FixedUpdate()
        {
            if (moveLength >= Range)
            {
                isMovingToTarget = false;
            }

            if (isMovingToTarget)
            {
                transform.position = Vector3.MoveTowards(transform.position, Target, Step);
                moveLength += Step;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, usePosition, Step);
                HeroTarget?.Player.Teleport(transform.position);
            }

            if (Vector3.Distance(transform.position, Target) < 0.5f)
            {
                isMovingToTarget = false;
            }

            if (isDestroying && Vector3.Distance(transform.position, usePosition) < 0.5f)
            {
                IsEnded = true;

                HeroTarget?.Player.DisableEffect<Ensnared>();
                Owner.Player.DisableEffect<Ensnared>();

                Timing.CallDelayed(0.2f, () =>
                {
                    HeroTarget.Player.IsGodModeEnabled = false;
                });
            }
        }
    }
}
