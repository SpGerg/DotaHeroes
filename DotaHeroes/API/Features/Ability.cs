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

        public abstract string Slug { get; }

        public abstract string Description { get; }

        public abstract string Lore { get; }

        public abstract AbilityType AbilityType { get; }

        public abstract TargetType TargetType { get; }

        public bool IsEnabled { get; set; } = true;

        public bool IsVisible { get; set; } = true;

        public bool IsStop { 
            get
            {
                return isStop;
            }
            private set
            {
                isStop = value;
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

        private int level = -1;

        /// <summary>
        /// Initializes a new instance of the <see cref="Ability" /> class.
        /// </summary>
        public Ability() { }

        /// <summary>
        /// Level up.
        /// </summary>
        public virtual void LevelUp(Hero hero)
        {
            Level++;
            if (this is ILevelValues)
            {
                var levelValues = this as ILevelValues;

                if (levelValues.Values.ContainsKey("cooldown"))
                {
                    var cooldown = Cooldowns.GetCooldown(hero.Player.Id, Slug);

                    if (cooldown != default)
                    {
                        cooldown.Duration = (float)levelValues.Values["cooldown"][Level];
                    }

                    Cooldowns.AddCooldown(hero.Player.Id, new CooldownInfo(Slug, (float)levelValues.Values["cooldown"][Level]));
                }

                if (levelValues.Values.ContainsKey("damage") && this is IDamage)
                {
                    (this as IDamage).Damage = levelValues.Values["damage"][Level];
                }

                if (levelValues.Values.ContainsKey("mana_cost") && this is ICost)
                {
                    (this as ICost).ManaCost = (int)levelValues.Values["mana_cost"][Level];
                }

                if (levelValues.Values.ContainsKey("health_cost") && this is ICost)
                {
                    (this as ICost).HealthCost = (int)levelValues.Values["health_cost"][Level];
                } //thats sucks im know
            }
        }

        /// <summary>
        /// Protected execute.
        /// </summary>
        protected virtual bool Execute(Hero executor, out string response, bool isCooldown = false)
        {
            if (!IsEnabled)
            {
                response = "Ability is disabled";

                return false;
            }

            if (Level < 0)
            {
                response = "This is ability is not upgraded";

                return false;
            }

            response = string.Empty;
            return true;
        }

        /// <summary>
        /// Stop. sToP.
        /// </summary>
        public virtual void Stop(Hero hero)
        {
            IsStop = true;
        }

        /// <summary>
        /// Check and run cooldown.
        /// </summary>
        protected void RunCooldown(Hero hero, CooldownInfo cooldown)
        {
            if (cooldown == default)
            {
                return;
            }

            cooldown.Run();
        }

        /// <summary>
        /// Check and run cooldown.
        /// </summary>
        protected bool CheckCooldown(Hero hero, out string response, out CooldownInfo cooldown)
        {
            var _cooldown = Cooldowns.GetCooldown(hero.Player.Id, Slug);

            if (this is ILevelValues levelValues && levelValues.Values.ContainsKey("cooldown"))
            {
                cooldown = _cooldown;

                if (cooldown != default && !cooldown.IsReady)
                {
                    response = $"Ability {Name} on cooldown.";

                    return false;
                }

                response = $"Ability {Name} was used.";
                return true;
            }

            cooldown = null;
            response = $"Ability {Name} was used.";
            return false;
        }

        public static List<Ability> ToAbilitiesFromStringList(List<string> abilties)
        {
            List<Ability> result = new List<Ability>();

            if (abilties == null || abilties.IsEmpty()) return result;

            foreach (var ability in abilties)
            {
                var _ability = API.GetAbilityOrDefaultBySlug(ability);

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
            stringBuilder.AppendLine("Cooldown: " + Cooldowns.ToStringIsCooldown(hero.Player.Id, Slug));

            return StringBuilderPool.Shared.ToStringReturn(stringBuilder);
        }
    }
}
