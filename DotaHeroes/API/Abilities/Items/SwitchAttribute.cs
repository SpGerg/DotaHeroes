﻿using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Interfaces;
using Exiled.API.Features;
using NorthwoodLib.Pools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Abilities.Items
{
    public class SwitchAttribute : ActiveAbility, ILevelValues
    {
        public override string Name => "Switch attribute";

        public override string Slug => "switch_attribute";

        public override string Description => "Switch attribute";

        public override string Lore => "Switch attribute";

        public override AbilityType AbilityType => AbilityType.Active;

        public override TargetType TargetType => TargetType.None;

        public AttributeType CurrentAttribute { get; private set; } = AttributeType.None;

        public Dictionary<string, List<decimal>> Values { get; } = Plugin.Instance.Config.Abilites["switch_attribute"].Values;

        public int MaxLevel { get; set; } = 1;

        public int MinLevel { get; set; } = 1;

        public IReadOnlyList<int> HeroLevelToLevelUp => throw new NotImplementedException();

        public SwitchAttribute() : base() { }

        public override void LevelUp(Hero hero) { }

        public void UpdateAttribute(Hero hero, AttributeType attribute)
        {
            if (CurrentAttribute != AttributeType.None)
            {
                hero.HeroStatistics.AddOrReduceStatistics(new Dictionary<StatisticsType, Value>
            {
                { Features.Utils.ToStatisticsType(CurrentAttribute), new Value(Values["given"][Level], false) }
            },
            true);
            }

            CurrentAttribute = attribute;

            hero.HeroStatistics.AddOrReduceStatistics(new Dictionary<StatisticsType, Value>
            {
                { Features.Utils.ToStatisticsType(CurrentAttribute), new Value(Values["given"][Level], false) }
            },
            false);

            Hud.Update(hero);
        }

        protected override bool Execute(Hero hero, ArraySegment<string> arguments, out string response)
        {
            //Strength -> Intelligence -> Agility -> Strength
            switch (CurrentAttribute)
            {
                case AttributeType.Strength:
                    response = $"Added {Values["given"][Level]} strength";
                    UpdateAttribute(hero, AttributeType.Intelligence); break;
                case AttributeType.Agility:
                    response = $"Added {Values["given"][Level]} agility";
                    UpdateAttribute(hero, AttributeType.Strength); break;
                case AttributeType.Intelligence:
                    response = $"Added {Values["given"][Level]} intelligence";
                    UpdateAttribute(hero, AttributeType.Agility); break;
                default:
                    response = "What";
                    break;
            }

            return true;
        }

        public override string ToStringHud(Hero hero)
        {
            StringBuilder stringBuilder = StringBuilderPool.Shared.Rent();
            stringBuilder.AppendLine("Name: " + Name);
            stringBuilder.AppendLine("Current attribute: " + CurrentAttribute);

            return StringBuilderPool.Shared.ToStringReturn(stringBuilder);
        }

        public override Ability Create()
        {
            return new SwitchAttribute();
        }
    }
}
