using DotaHeroes.API.Effects.Pudge;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Features.Components;
using DotaHeroes.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.GridLayoutGroup;

namespace DotaHeroes.API.Features.Objects
{
    public class RotObject : MonoBehaviour, IDamage, ICastRange
    {
        public HeroController Owner { get; private set; }

        public int Damage { get; set; }

        public DamageType DamageType { get; set; }

        public int Range { get; set; } = 5;

        public void Initialization(HeroController heroController, int range, int damage, DamageType damageType)
        {
            Owner = heroController;
            Range = range;
            Damage = damage;
            DamageType = damageType;
        }

        public void Update()
        {
            transform.position = Owner.Hero.Player.Transform.position;

            if (Owner.Hero.GetEffectOrDefault<Rot>() == default)
            {
                Destroy(gameObject);
            }
        }

        public void OnTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent(out HeroController heroController))
            {
                heroController.Hero.EnableEffect(new Rot(heroController.Hero.Player));
            }
        }

        public void OnTriggerExit(Collider collider)
        {
            if (collider.TryGetComponent(out HeroController heroController))
            {
                heroController.Hero.DisableEffect<Rot>();
            }
        }
    }
}
