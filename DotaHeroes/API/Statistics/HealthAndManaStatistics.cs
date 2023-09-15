using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Statistics
{
    public class HealthAndManaStatistics
    {
        public float MaximumMana { get; set; }

        public float MaximumHealth { get; set; }

        public float Health { get; set; }

        public float Mana { get; set; }

        public float HealthRegeneration { get; set; }

        public float ManaRegeneration { get; set; }

        public HealthAndManaStatistics() { }

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
            return $"Health: <color=Red>{Health}</color> Regen: <color=Red>{HealthRegeneration}</color> Mana: <color=#00FFFF>{Mana}</color> Regen: <color=Red>{ManaRegeneration}</color>";
        }
    }
}
