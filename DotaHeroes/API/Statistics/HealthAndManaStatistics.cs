using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Statistics
{
    public class HealthAndManaStatistics
    {
        public float MaximumHealth {
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

        public float MaximumMana
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

        public float Health { get; set; }

        public float Mana { get; set; }

        public float HealthRegeneration { get; set; }

        public float ManaRegeneration { get; set; }

        private float maximumMana;

        private float maximumHealth;

        public HealthAndManaStatistics() { }

        public HealthAndManaStatistics(float maximumHealth, float maximumMana)
        {
            MaximumMana = maximumMana;
            MaximumHealth = maximumHealth;
            Health = maximumHealth;
            Mana = maximumMana;
        }

        public HealthAndManaStatistics(float maximumHealth, float maximumMana, float health, float mana)
        {
            MaximumMana = maximumMana;
            MaximumHealth = maximumHealth;
            Health = health;
            Mana = mana;
        }

        public HealthAndManaStatistics(float maximumHealth, float maximumMana, float health, float mana, float healthReg, float manaReg)
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
            return $"Health: <color=Red>{(int)Health}</color> Regen: <color=Red>{Math.Round(HealthRegeneration, 1)}</color> Mana: <color=#00FFFF>{(int)Mana}</color> Regen: <color=Red>{Math.Round(ManaRegeneration, 1)}</color>";
        }
    }
}
