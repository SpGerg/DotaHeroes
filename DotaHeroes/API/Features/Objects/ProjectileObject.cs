using DotaHeroes.API.Enums;
using DotaHeroes.API.Features.Components;
using DotaHeroes.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DotaHeroes.API.Features.Objects
{
    [RequireComponent(typeof(BoxCollider))]
    public class ProjectileObject : MonoBehaviour, IDamage
    {
        public HeroController Target { get; set; }

        public int Damage { get; set; }

        public DamageType DamageType { get; set; }

        public float Speed { get; set; }

        public float Step { get; private set; }

        private Vector3 lastPosition { get; set; }

        public void Start()
        {
            Step = Speed * Time.deltaTime;
        }

        public void Initialization(HeroController heroController, int damage, DamageType damageType, float speed)
        {
            Target = heroController;
            Damage = damage;
            DamageType = damageType;
            Speed = speed;
        }

        public void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, lastPosition, Step);

            if (Target == null)
            {
                return;
            }

            lastPosition = Target.transform.position;

            if (transform.position == lastPosition)
            {
                Target.TakeDamage(Damage, DamageType);

                Destroy(gameObject);
            }
        }
    }
}
