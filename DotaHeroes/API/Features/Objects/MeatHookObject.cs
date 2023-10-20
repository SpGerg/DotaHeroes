using CustomPlayerEffects;
using DotaHeroes.API.Abilities.Pudge;
using DotaHeroes.API.Effects;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Interfaces;
using Exiled.API.Features;
using MEC;
using Mirror;
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

        public Player MovingSound { get; set; }

        private bool isMovingToTarget { get; set; } = true;

        private Vector3 usePosition { get; set; }

        public bool IsEnded { get; set; }

        private float moveLength { get; set; }

        private bool isDestroying { get; set; }

        public void Initialize(Hero owner, Vector3 target, int range, float speed, int damage, DamageType damageType, Player movingSound)
        {
            Owner = owner;
            Target = target;
            Range = range;
            Speed = speed;
            Damage = damage;
            DamageType = damageType;
            MovingSound = movingSound;

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
                if (hero == Owner) continue;

                if (Vector3.Distance(transform.position, hero.Player.Position) < 1)
                {
                    if (hero.Player.UserId == Owner.Player.UserId)
                    {
                        return;
                    }

                    isMovingToTarget = false;

                    HeroTarget = hero;
                    HeroTarget.EnableEffect(new Stun(HeroTarget), 0);

                    Audio.Stop(MovingSound);
                    Audio.Play(HeroTarget.Player.Position, MeatHook.SoundsPath + "\\hooked.ogg");
                    Audio.Play(Owner.Player.Position, MeatHook.SoundsPath + "\\hooked.ogg");
                    Audio.Play(HeroTarget.Player.Position, MeatHook.SoundsPath + "\\moving_to_target.ogg", 50f);
                    Audio.Play(Owner.Player.Position, MeatHook.SoundsPath + "\\moving_to_target.ogg", 50f);

                    if (hero.SideType == Owner.SideType)
                    {
                        return;
                    }

                    hero.TakeDamage(Owner, Damage, DamageType);
                }
            }
        }

        public void FixedUpdate()
        {
            if (moveLength >= Range)
            {
                isMovingToTarget = false;
                Audio.Stop(MovingSound);
                Audio.Play(Owner.Player.Position, MeatHook.SoundsPath + "\\moving_to_target.ogg");
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

                HeroTarget?.DisableEffect<Stun>();
                Owner.Player.DisableEffect<Ensnared>();
            }
        }
    }
}
