using DotaHeroes.API.Enums;
using DotaHeroes.API.Heroes;
using DotaHeroes.API.Statistics;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace DotaHeroes.API.Features.Serializables
{
    public class HeroSerializable
    {
        public virtual List<RoleTypeId> ChangeRoles { get; set; }

        public List<string> Abilties { get; set; }

        public HeroClassType HeroClassType { get; set; }

        public HeroStatisticsSerializable DefaultHeroStatistics { get; set; }

        public bool IsRegistering { get; set; } = true;

        [YamlIgnore]
        private Hero hero { get; set; }

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
