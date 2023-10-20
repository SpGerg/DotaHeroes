using YamlDotNet.Serialization;

namespace DotaHeroes.API.Statistics
{
    public class AttackStatistics
    {
        public int BaseAttackDamage { get; }

        public int AttackDamage { get; set; }

        public int ExtraAttackDamage { get; set; } 

        [YamlIgnore]
        public int FullDamage {
            get
            {
                return AttackDamage + ExtraAttackDamage;
            }
        }

        public int AttackSpeed { get; set; } //In future..

        public decimal AttackRange { get; set; }

        public decimal ProjectileSpeed { get; set; }

        public AttackStatistics() { }

        public AttackStatistics(int baseAttackDamage, int extraAttackDamage, int attackSpeed, decimal attackRange, decimal projectileSpeed = 0)
        {
            BaseAttackDamage = baseAttackDamage;
            ExtraAttackDamage = extraAttackDamage;
            AttackSpeed = attackSpeed;
            AttackRange = attackRange;
            ProjectileSpeed = projectileSpeed;
        }

        public override string ToString()
        {
            if (ExtraAttackDamage > 0)
            {
                return $"Attack damage: {AttackDamage} + <color=Green>{ExtraAttackDamage}</color>";
            }

            return $"Attack damage: {AttackDamage}";
        }
    }
}
