using DotaHeroes.API.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DotaHeroes.API.Features.Components
{
    public class HeroController : MonoBehaviour
    {
        public Hero Hero { get; set; }

        public void TakeDamage(float damage, DamageType damageType)
        {
            switch (damageType)
            {
                case DamageType.None: break;
                case DamageType.Physical:
                    float armor = Hero.HeroStatistics.Armor.BaseArmor + Hero.HeroStatistics.Armor.MoreArmor;
                    float armor_percent = (0.052f * armor) / (0.9f + 0.048f * armor);
                    int physical_total_damage = (int)(damage - ((damage / 100) * armor_percent));

                    ReduceHealthAndCheckForDead(physical_total_damage);

                    break;
                case DamageType.Magical:
                    ReduceHealthAndCheckForDead((int)(damage - (damage / 100) * Hero.HeroStatistics.Resistance.MagicResistance));

                    break;
                case DamageType.Pure:
                    ReduceHealthAndCheckForDead(damage);

                    break;
            }
        }

        private void ReduceHealthAndCheckForDead(float damage)
        {
            Hero.HeroStatistics.HealthAndMana.Health -= damage;

            if (Hero.HeroStatistics.HealthAndMana.Health < 0)
            {
                Hero.IsDead = true;
            }
        }
    }
}
