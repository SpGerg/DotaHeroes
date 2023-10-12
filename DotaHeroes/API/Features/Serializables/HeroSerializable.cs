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
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual List<RoleTypeId> ChangeRoles { get; set; }

        public List<string> Abilties { get; set; }

        public HeroClassType HeroClassType { get; set; }

        public HeroStatisticsSerializable DefaultHeroStatistics { get; set; }

        [YamlIgnore]
        private Hero hero { get; set; }

        public HeroSerializable()
        {
            Name = string.Empty;
            Description = string.Empty;
            ChangeRoles = new List<RoleTypeId>();
            Abilties = new List<string>();
            HeroClassType = HeroClassType.Ranged;
            DefaultHeroStatistics = new HeroStatisticsSerializable();
        }

        public HeroSerializable(string name, string description, List<string> abilties, List<RoleTypeId> changeRoles, HeroClassType heroClassType, HeroStatisticsSerializable heroStatistics)
        {
            Name = name;
            Description = description;
            Abilties = abilties;
            ChangeRoles = changeRoles;
            HeroClassType = heroClassType;
            DefaultHeroStatistics = heroStatistics;
        }

        public Hero ToHero()
        {
            if (hero is null)
            {
                hero = API.GetRegisteredHeroes().Values.FirstOrDefault(_hero => _hero.HeroName == Name);
            }

            return hero;
        }
    }
}
