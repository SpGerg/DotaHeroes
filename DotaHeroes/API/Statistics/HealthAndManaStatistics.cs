using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Statistics
{
    public class HealthAndManaStatistics
    {
        public decimal MaximumHealth {
            get
            {
                return maximumHealth;
            }
            set
            {
                var oldValue = maximumHealth;
                maximumHealth = value;
                Health += maximumHealth - oldValue;

                if (Health < 0)
                {
                    Health = 1;
                }
            }
        }

        public decimal MaximumMana
        {
            get
            {
                return maximumMana;
            }
            set
            {
                var oldValue = maximumMana;
                maximumMana = value;
                Mana += maximumMana - oldValue;

                if (Mana < 0)
                {
                    Mana = 1;
                }
            }
        }

        public decimal Health { get; set; }

        public decimal Mana { get; set; }

        public decimal HealthRegeneration { get; set; }

        public decimal ManaRegeneration { get; set; }

        public decimal BaseHealth { get; }

        public decimal BaseHealthRegeneration { get; }

        public decimal BaseMana { get; }

        public decimal BaseManaRegeneration { get; }

        private decimal maximumMana;

        private decimal maximumHealth;

        public HealthAndManaStatistics() { }

        public HealthAndManaStatistics(decimal maximumHealth, decimal maximumMana)
        {
            MaximumMana = maximumMana;
            MaximumHealth = maximumHealth;
            BaseHealth = maximumHealth;
            BaseMana = maximumMana;
            Health = maximumHealth;
            Mana = maximumMana;
        }

        public HealthAndManaStatistics(decimal maximumHealth, decimal maximumMana, decimal health, decimal mana)
        {
            MaximumMana = maximumMana;
            MaximumHealth = maximumHealth;
            BaseHealth = maximumHealth;
            BaseMana = maximumMana;
            Health = health;
            Mana = mana;
        }

        public HealthAndManaStatistics(decimal maximumHealth, decimal maximumMana, decimal health, decimal mana, decimal healthReg, decimal manaReg)
        {
            MaximumMana = maximumMana;
            MaximumHealth = maximumHealth;
            BaseHealth = maximumHealth;
            BaseHealthRegeneration = healthReg;
            BaseMana = maximumMana;
            BaseManaRegeneration = manaReg;
            Health = health;
            Mana = mana;
            HealthRegeneration = healthReg;
            ManaRegeneration = manaReg;
        }

        public override string ToString()
        {
            return $"Health: <color=Red>{Health}</color> Regen: <color=Red>{Math.Round(HealthRegeneration, 1)}</color> Mana: <color=#00FFFF>{Mana}</color> Regen: <color=Red>{Math.Round(ManaRegeneration, 1)}</color>";
        }
    }
}
