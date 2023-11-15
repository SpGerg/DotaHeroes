using DotaHeroes.API.Enums;
using DotaHeroes.API.Interfaces;
using Exiled.API.Features;
using NorthwoodLib.Pools;
using System.Collections.Generic;
using System.Text;

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

        public Hero Owner { get; }

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

        public int Level { get; set; } = -1;

        private bool isStop;

        /// <summary>
        /// Initializes a new instance of the <see cref="Ability" /> class.
        /// </summary>
        public Ability()
        {
            if (this is ILevelValues levelValues)
            {
                Level = levelValues.MinLevel - 1;
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Ability" /> class.
        /// </summary>
        /// <param name="hero"><inheritdoc cref="Hero" /></param>
        public Ability(Hero hero) : this() 
        {
            Owner = hero;
        }

        /// <summary>
        /// Level up.
        /// </summary>
        public virtual void LevelUp()
        {
            Level++;
            if (this is ILevelValues levelValues)
            {
                if (levelValues.Values.ContainsKey("cooldown"))
                {
                    var cooldown = Cooldowns.GetCooldown(Owner.Player.Id, Slug);

                    if (cooldown != default)
                    {
                        cooldown.Duration = (float)levelValues.Values["cooldown"][Level];
                    }

                    Cooldowns.AddCooldown(Owner.Player.Id, new CooldownInfo(Slug, (float)levelValues.Values["cooldown"][Level]));
                }

                if (levelValues.Values.ContainsKey("damage") && this is IDamage)
                {
                    (this as IDamage).Damage = levelValues.Values["damage"][Level];
                }

                if (this is ICost cost)
                {
                    if (levelValues.Values.ContainsKey("mana_cost"))
                    {
                        cost.ManaCost = (int)levelValues.Values["mana_cost"][Level];
                    }

                    if (levelValues.Values.ContainsKey("health_cost"))
                    {
                        cost.HealthCost = (int)levelValues.Values["health_cost"][Level];
                    } //thats sucks im know
                }
            }
        }

        /// <summary>
        /// Protected execute.
        /// </summary>
        protected virtual bool Execute(out string response, bool isCooldown = false)
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
        public virtual void Stop()
        {
            IsStop = true;
        }

        /// <summary>
        /// Check and run cooldown.
        /// </summary>
        protected void RunCooldown(CooldownInfo cooldown)
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
        protected bool CheckCooldown(out string response, out CooldownInfo cooldown)
        {
            if (Owner == null)
            {
                cooldown = null;
                response = "Owner is null";
                return false;
            }

            var _cooldown = Cooldowns.GetCooldown(Owner.Player.Id, Slug);
            cooldown = _cooldown;

            if (this is ILevelValues levelValues && levelValues.Values.TryGetValue("cooldown", out List<decimal> result))
            {
                if (cooldown == default)
                {
                    cooldown = Cooldowns.AddCooldown(Owner.Player.Id, new CooldownInfo(Slug, (float)result[Level]));
                }

                if (!cooldown.IsReady)
                {
                    response = $"Ability {Name} on cooldown. Cooldown: {cooldown.Cooldown}";

                    return false;
                }
            }

            response = $"Ability {Name} was used.";
            return true;
        }

        public static List<Ability> ToAbilitiesFromStringList(Hero hero, List<string> abilties, bool isCreate = false)
        {
            List<Ability> result = new List<Ability>();

            if (abilties == null || abilties.IsEmpty()) return result;

            foreach (var ability in abilties)
            {
                var _ability = DTAPI.GetAbilityOrDefaultBySlug(ability);

                if (_ability == default) continue;
                
                result.Add(isCreate ? _ability.Create(hero) : _ability);
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

        /// <summary>
        /// To string hud.
        /// </summary>
        public virtual string ToStringHud(Hero hero)
        {
            StringBuilder stringBuilder = StringBuilderPool.Shared.Rent();
            stringBuilder.AppendLine("Name: " + Name);
            stringBuilder.AppendLine("Level: " + (Level + 1));

            var cooldown = Cooldowns.GetCooldown(hero.Player.Id, Slug);

            if (cooldown != default)
            {
                if (cooldown.Cooldown == 0)
                {
                    stringBuilder.AppendLine("Cooldown: <color=Green>Ready</color>");
                }
                else
                {
                    stringBuilder.AppendLine("Cooldown: " + cooldown.Cooldown);
                }
            }

            if (this is ToggleAbility toggleAbility)
            {
                stringBuilder.AppendLine("Active: " + toggleAbility.ToStringIsActive());
            }

            return StringBuilderPool.Shared.ToStringReturn(stringBuilder);
        }

        public abstract Ability Create(Hero hero);
    }
}
