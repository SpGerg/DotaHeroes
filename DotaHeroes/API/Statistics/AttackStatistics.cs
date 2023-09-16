﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Statistics
{
    public class AttackStatistics
    {
        public int BaseAttackDamage { get; set; }

        public int MoreAttackDamage { get; set; }

        public int AttackSpeed { get; set; } //In future..

        public float AttackRange { get; set; }

        public AttackStatistics() { }

        public AttackStatistics(int baseAttackDamage, int moreAttackDamage, int attackSpeed, float attackRange)
        {
            BaseAttackDamage = baseAttackDamage;
            MoreAttackDamage = moreAttackDamage;
            AttackSpeed = attackSpeed;
            AttackRange = attackRange;
        }

        public override string ToString()
        {
            if (MoreAttackDamage > 0)
            {
                return $"Attack damage: {BaseAttackDamage} + <color=Green>{MoreAttackDamage}</color>";
            }

            return $"Attack damage: {BaseAttackDamage}";
        }
    }
}
