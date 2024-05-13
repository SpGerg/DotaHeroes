using System;

namespace DotaHeroes.API.Statistics
{
    public class HealthAndManaStatistics
    {
        public double MaximumHealth
        {
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

        public double MaximumMana
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

        public double Health { get; set; }

        public double Mana { get; set; }

        public double HealthRegeneration { get; set; }

        public double ManaRegeneration { get; set; }

        public double BaseHealth { get; }

        public double BaseHealthRegeneration { get; }

        public double BaseMana { get; }

        public double BaseManaRegeneration { get; }

        private double maximumMana;

        private double maximumHealth;

        public HealthAndManaStatistics() { }

        public HealthAndManaStatistics(double maximumHealth, double maximumMana)
        {
            MaximumMana = maximumMana;
            MaximumHealth = maximumHealth;
            BaseHealth = maximumHealth;
            BaseMana = maximumMana;
            Health = maximumHealth;
            Mana = maximumMana;
        }

        public HealthAndManaStatistics(double maximumHealth, double maximumMana, double health, double mana)
        {
            MaximumMana = maximumMana;
            MaximumHealth = maximumHealth;
            BaseHealth = maximumHealth;
            BaseMana = maximumMana;
            Health = health;
            Mana = mana;
        }

        public HealthAndManaStatistics(double maximumHealth, double maximumMana, double health, double mana, double healthReg, double manaReg)
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
