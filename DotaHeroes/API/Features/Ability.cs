using CommandSystem;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Extensions;
using DotaHeroes.API.Features;
using DotaHeroes.API.Interfaces;
using Exiled.API.Features;
using NorthwoodLib.Pools;
using RemoteAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Features
{
    public abstract class Ability
    {
        public abstract string Name { get; }

        public abstract string Description { get; }

        public abstract string Lore { get; }

        public abstract AbilityType AbilityType { get; }

        public abstract TargetType TargetType { get; }

        public abstract int MaxLevel { get; }

        public bool IsEnabled { get; set; } = true;

        public bool IsStop { 
            get
            {
                return IsStop;
            }
            set
            {
                isStop = value;

                if (isStop)
                {
                    Stop();
                }
            }
        }

        private bool isStop { get; set; }

        public int Level
        {
            get
            {
                return level;
            }
            set
            {
                level = value;

                if (level > MaxLevel)
                {
                    level = MaxLevel;
                }
            }
        }

        private int level;

        public Ability() { }

        public virtual void LevelUp(Hero hero)
        {
            Level++;
            if (this is IValues && (this as IValues).Values.ContainsKey("cooldowns"))
            {
                var cooldown = Cooldowns.GetCooldown(hero.Player.UserId, Name);

                if (cooldown == default)
                {
                    cooldown = Cooldowns.AddCooldown(hero.Player.UserId, new CooldownInfo(Name, 3));
                }

                cooldown.Duration = (int)(this as IValues).Values["cooldown"][Level];
            }
        }

        protected virtual bool Execute(ICommandSender sender, out string response, out Hero hero, bool isCooldown = false)
        {
            if (!IsEnabled)
            {
                response = "Ability is disabled";
                hero = null;

                return false;
            }

            if (sender is not PlayerCommandSender)
            {
                response = "You cannot use this ability in console.";
                hero = null;

                return false;
            }

            var player = Player.Get(sender);

            var _hero = API.GetHeroOrDefault(player.UserId);

            if (_hero == default)
            {
                response = "You are not hero. (Im mean in game, ok?)";
                hero = null;

                return false;
            }

            hero = _hero;

            if (_hero.Abilities.FirstOrDefault(ability => ability.Name == Name) == default)
            {
                response = "You havent this ability.";
                return false;
            }

            if ((this as IValues).Values.ContainsKey("cooldown"))
            {
                var cooldown = Cooldowns.GetCooldown(hero.Player.UserId, Name);

                if (cooldown == default)
                {
                    cooldown = Cooldowns.AddCooldown(hero.Player.UserId, new CooldownInfo(Name, 3));
                }

                if (!cooldown.IsReady)
                {
                    Log.Info(cooldown.Cooldown);
                    response = $"Ability {Name} on cooldown.";

                    return false;
                }

                response = $"Ability {Name} was used.";

                if (isCooldown && this is IValues)
                {
                    if ((this as IValues).Values.ContainsKey("cooldown"))
                    {
                        cooldown.Run();
                    }
                }
            }

            response = string.Empty;

            return true;
        }

        public virtual void Stop()
        {

        }

        public override string ToString()
        {
            StringBuilder stringBuilder = StringBuilderPool.Shared.Rent();

            stringBuilder.Append("Name: " + Name);
            stringBuilder.Append("Description: " + Description);
            stringBuilder.Append("Lore: " + Lore);
            stringBuilder.Append("Ability Type: " + AbilityType.ToString());
            stringBuilder.Append("Target Type: " + TargetType.ToStringWithSpaces());

            return StringBuilderPool.Shared.ToStringReturn(stringBuilder);
        }
    }
}
