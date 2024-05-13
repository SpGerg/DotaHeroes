using DotaHeroes.API.Enums;
using PlayerRoles;
using System.Collections.Generic;

namespace DotaHeroes.API.Features.Serializables
{
    public class HeroSerializable
    {
        public virtual List<RoleTypeId> ChangeRoles { get; set; }

        public List<string> Abilties { get; set; }

        public HeroClassType HeroClassType { get; set; }

        public HeroStatisticsSerializable DefaultHeroStatistics { get; set; }

        public bool IsRegistering { get; set; } = true;

        public HeroSerializable()
        {
            ChangeRoles = new List<RoleTypeId>();
            Abilties = new List<string>();
            HeroClassType = HeroClassType.Ranged;
            DefaultHeroStatistics = new HeroStatisticsSerializable();
        }

        public HeroSerializable(List<string> abilties, List<RoleTypeId> changeRoles, HeroClassType heroClassType, HeroStatisticsSerializable heroStatistics)
        {
            Abilties = abilties;
            ChangeRoles = changeRoles;
            HeroClassType = heroClassType;
            DefaultHeroStatistics = heroStatistics;
        }
    }
}
