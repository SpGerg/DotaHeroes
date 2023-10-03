using CommandSystem;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Events.Handlers;
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
using UnityEngine;

namespace DotaHeroes.API.Features
{
    public abstract class Ability
    {
        public abstract string Name { get; }

        public abstract string Description { get; }

        public abstract string Lore { get; }

        public abstract AbilityType AbilityType { get; }

        public abstract TargetType TargetType { get; }

        public bool IsEnabled { get; set; } = true;

        public bool IsVisible { get; set; } = true;

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
            }
        }

        private int level;

        /// <summary>
        /// Initializes a new instance of the <see cref="Ability" /> class.
        /// </summary>
        public Ability() { }

        /// <summary>
        /// Level up, if class is inheritance from IValues and contains cooldowns. Automatically set cooldown duration.
        /// </summary>
        public virtual void LevelUp(Hero hero)
        {
            Level++;
            if (this is ILevelValues && (this as ILevelValues).Values.ContainsKey("cooldowns"))
            {
                var cooldown = Cooldowns.GetCooldown(hero.Player.Id, Name);

                if (cooldown == default)
                {
                    cooldown = Cooldowns.AddCooldown(hero.Player.Id, new CooldownInfo(Name, 3));
                }

                cooldown.Duration = (int)(this as ILevelValues).Values["cooldown"][Level];
            }
        }

        /// <summary>
        /// Protected execute.
        /// </summary>
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

            var _hero = API.GetHeroOrDefault(player.Id);

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

            response = string.Empty;
            return true;
        }

        /// <summary>
        /// Stop. sToP.
        /// </summary>
        public virtual void Stop()
        {

        }

        /// <summary>
        /// Check and run cooldown.
        /// </summary>
        protected bool CheckAndRunCooldown(Hero hero, out string response)
        {
            if (this is ILevelValues && (this as ILevelValues).Values.ContainsKey("cooldown"))
            {
                var cooldown = Cooldowns.GetCooldown(hero.Player.Id, Name);

                if (cooldown == default)
                {
                    cooldown = Cooldowns.AddCooldown(hero.Player.Id, new CooldownInfo(Name, 3));
                }

                if (!cooldown.IsReady)
                {
                    response = $"Ability {Name} on cooldown.";

                    return false;
                }

                cooldown.Run();
            }

            response = $"Ability {Name} was used.";
            return true;
        }

        public static List<Ability> ToAbilitiesFromStringList(List<string> abilties)
        {
            List<Ability> result = new List<Ability>();

            foreach (var ability in abilties)
            {
                var _ability = API.GetAbilityOrDefaultWithIgnoreCaseAndSpaces(ability);

                if (_ability == default) continue;
                
                result.Add(_ability);
            }

            return result;
        }

        /// <summary>
        /// To string.
        /// </summary>
        public override string ToString()
        {
            StringBuilder stringBuilder = StringBuilderPool.Shared.Rent();

            stringBuilder.AppendLine("Name: " + Name);
            stringBuilder.AppendLine("Description: " + Description);
            stringBuilder.AppendLine("Lore: " + Lore);
            stringBuilder.AppendLine("Ability Type: " + AbilityType.ToString());
            stringBuilder.AppendLine("Target Type: " + TargetType.ToString());

            return StringBuilderPool.Shared.ToStringReturn(stringBuilder);
        }

        /// <summary>
        /// To string with hero.
        /// </summary>
        public string ToString(Hero hero)
        {
            StringBuilder stringBuilder = StringBuilderPool.Shared.Rent();

            stringBuilder.AppendLine("Name: " + Name);
            stringBuilder.AppendLine("Description: " + Description);
            stringBuilder.AppendLine("Lore: " + Lore);
            stringBuilder.AppendLine("Ability Type: " + AbilityType.ToString());
            stringBuilder.AppendLine("Target Type: " + TargetType.ToString());
            stringBuilder.AppendLine("Cooldown: " + Cooldowns.ToStringIsCooldown(hero.Player.Id, Name));

            return StringBuilderPool.Shared.ToStringReturn(stringBuilder);
        }
    }
}
