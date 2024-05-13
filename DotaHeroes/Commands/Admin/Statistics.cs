using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using Exiled.API.Features;
using Exiled.API.Features.Pools;
using System;

namespace DotaHeroes.Commands.Admin
{
    public class Statistics : AdminCommandBase
    {
        public override string Command => "stats";

        public override string Description => "Add or reduce some statistics for hero";

        public override bool Execute(Player player, ArraySegment<string> arguments, out string response)
        {
            if (arguments.Count <= 2)
            {
                var stringBuilder = StringBuilderPool.Pool.Get();

                stringBuilder.AppendLine("Command format: stats <type> <value> <isReduce>");
                stringBuilder.AppendLine("Command format example: stats agility 3 false");

                stringBuilder.AppendLine("List of all statistics type: ");

                foreach (var value in Enum.GetNames(typeof(StatisticsType)))
                {
                    stringBuilder.AppendLine(value);
                }

                response = StringBuilderPool.Pool.ToStringReturn(stringBuilder);
                return false;
            }

            if (!Enum.TryParse(arguments.Array[2], true, out StatisticsType statisticsType))
            {
                var stringBuilder = StringBuilderPool.Pool.Get();

                stringBuilder.AppendLine("List of all statistics: ");

                foreach (var value in Enum.GetNames(typeof(StatisticsType)))
                {
                    stringBuilder.AppendLine(value);
                }

                response = StringBuilderPool.Pool.ToStringReturn(stringBuilder);
                return false;
            }

            if (!float.TryParse(arguments.Array[3], out float _value))
            {
                response = "Second argument must be number";
                return false;
            }

            if (!bool.TryParse(arguments.Array[4], out bool isReduce))
            {
                response = "Third argument must be bool. True (yes) or false (no).";
                return false;
            }

            if (!API.Features.Utils.GetHeroFromPlayerEyeDirection(player, 5, out response, out Hero hero))
            {
                return false;
            }

            response = $"Have been added {_value} for {statisticsType}";
            hero.HeroStatistics.AddOrReduceStatistic(statisticsType, new Value((double)_value, false), isReduce);

            Log.Info($"Have been added {_value} for {statisticsType} by {player.Nickname}");
            return true;
        }
    }
}
