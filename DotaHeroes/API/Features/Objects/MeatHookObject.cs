using CustomPlayerEffects;
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
        public HeroController Owner { get; private set; }

        public HeroController HeroTarget { get; private set; }

        public Vector3 Target { get; private set; }

        public int Range { get; set; }

        public float Step { get; private set; }

        public float Speed { get; private set; }

        public int Damage { get; set; }

        public bool IsEnded { get; set; }

        public DamageType DamageType { get; set; }

        private bool isMovingToTarget { get; set; } = true;

        private Vector3 usePosition { get; set; }

        private float moveLength { get; set; }

        private bool isDestroying { get; set; }

        public void Initialization(HeroController owner, Vector3 target, int range, float speed, int damage, DamageType damageType)
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
                HeroTarget?.Hero.Player.Teleport(transform.position);
            }

            if (Vector3.Distance(transform.position, Target) < 0.5f)
            {
                isMovingToTarget = false;
            }

            if (isDestroying && Vector3.Distance(transform.position, usePosition) < 0.5f)
            {
                IsEnded = true;

                HeroTarget?.Hero.Player.DisableEffect<Ensnared>();
                Owner.Hero.Player.DisableEffect<Ensnared>();

                Timing.CallDelayed(0.2f, () =>
                {
                    HeroTarget.Hero.Player.IsGodModeEnabled = false;
                });
            }
        }

        public void OnTriggerEnter(Collider collider)
        {
            if (isMovingToTarget && collider.TryGetComponent(out HeroController heroController))
            {
                if (heroController.Hero.Player.UserId == Owner.Hero.Player.UserId)
                {
                    return;
                }

                isMovingToTarget = false;

                HeroTarget = heroController;
                HeroTarget.Hero.Player.IsGodModeEnabled = true;
                HeroTarget.Hero.Player.EnableEffect<Ensnared>();

                if (heroController.Hero.SideType == Owner.Hero.SideType)
                {
                    return;
                }

                heroController.Hero.TakeDamage(Damage, DamageType);
            }
        }
    }
}
