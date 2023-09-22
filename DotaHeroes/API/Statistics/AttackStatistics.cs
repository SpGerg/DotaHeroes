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

        public int ExtraAttackDamage { get; set; }

        public int FullDamage {
            get
            {
                return BaseAttackDamage + ExtraAttackDamage;
            }
        }

        public int AttackSpeed { get; set; } //In future..

        public float AttackRange { get; set; }

        public AttackStatistics() { }

        public AttackStatistics(int baseAttackDamage, int extraAttackDamage, int attackSpeed, float attackRange)
        {
            BaseAttackDamage = baseAttackDamage;
            ExtraAttackDamage = extraAttackDamage;
            AttackSpeed = attackSpeed;
            AttackRange = attackRange;
        }

        public override string ToString()
        {
            if (ExtraAttackDamage > 0)
            {
                return $"Attack damage: {BaseAttackDamage} + <color=Green>{ExtraAttackDamage}</color>";
            }

            return $"Attack damage: {BaseAttackDamage}";
        }
    }
}
