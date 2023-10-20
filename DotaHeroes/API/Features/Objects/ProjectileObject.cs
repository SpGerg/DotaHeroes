using DotaHeroes.API.Enums;
using DotaHeroes.API.Interfaces;
using Mirror;
using UnityEngine;

namespace DotaHeroes.API.Features.Objects
{
    [RequireComponent(typeof(BoxCollider))]
    public class ProjectileObject : MonoBehaviour, IDamage
    {
        public Hero Owner { get; set; }

        public Hero Target { get; set; }

        public decimal Damage { get; set; }

        public DamageType DamageType { get; set; }

        public float Speed { get; set; }

        public float Step { get; private set; }

        public bool IsIgnoreClear { get; set; }

        private Vector3 lastPosition { get; set; }

        public void Start()
        {
            Step = Speed * Time.deltaTime;
        }

        public void Initialize(Hero owner, Hero target, int damage, DamageType damageType, float speed, bool isIgnoreClear = false)
        {
            Owner = owner;
            Target = target;
            Damage = damage;
            DamageType = damageType;
            Speed = speed;
            IsIgnoreClear = isIgnoreClear;
        }

        public void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, lastPosition, Step);

            if (Vector3.Distance(transform.position, lastPosition) < 1)
            {
                Owner?.Attack(Target);

                NetworkServer.Destroy(gameObject);
            }

            if (Target == null)
            {
                return;
            }

            lastPosition = Target.Player.Position;
        }
    }
}
