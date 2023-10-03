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
            }
        }

        public decimal Health { get; set; }

        public decimal Mana { get; set; }

        public decimal HealthRegeneration { get; set; }

        public decimal ManaRegeneration { get; set; }

        private decimal maximumMana;

        private decimal maximumHealth;

        public HealthAndManaStatistics() { }

        public HealthAndManaStatistics(int maximumHealth, int maximumMana)
        {
            MaximumMana = maximumMana;
            MaximumHealth = maximumHealth;
            Health = maximumHealth;
            Mana = maximumMana;
        }

        public HealthAndManaStatistics(int maximumHealth, int maximumMana, int health, int mana)
        {
            MaximumMana = maximumMana;
            MaximumHealth = maximumHealth;
            Health = health;
            Mana = mana;
        }

        public HealthAndManaStatistics(int maximumHealth, int maximumMana, int health, int mana, int healthReg, int manaReg)
        {
            MaximumMana = maximumMana;
            MaximumHealth = maximumHealth;
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
