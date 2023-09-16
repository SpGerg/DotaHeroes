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

        public int TakeDamage(int damage, DamageType damageType)
        {
            int total_damage = 0;

            switch (damageType)
            {
                case DamageType.None: break;
                case DamageType.Physical:
                    float armor = Hero.HeroStatistics.Armor.BaseArmor + Hero.HeroStatistics.Armor.MoreArmor;
                    float armor_percent = (0.052f * armor) / (0.9f + 0.048f * armor);
                    total_damage = (int)(damage - ((damage / 100) * armor_percent));

                    break;
                case DamageType.Magical:
                    total_damage = (int)(damage - (damage / 100) * Hero.HeroStatistics.Resistance.MagicResistance);

                    break;
                case DamageType.Pure:
                    total_damage = (int)damage;
                    break;
            }

            ReduceHealthAndCheckForDead(total_damage);

            return total_damage;
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
