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

namespace DotaHeroes.API
{
    public abstract class Ability
    {
        public abstract string Name { get; }

        public abstract string Description { get; }

        public abstract string Lore { get; }

        public abstract AbilityType AbilityType { get; }

        public abstract TargetType TargetType { get; }

        public abstract int MaxLevel { get; }

        public virtual Cooldown Cooldown { get; } = new Cooldown(3);

        public Player Owner { get; }

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

        public Ability(Player owner)
        {
            Owner = owner;
        }

        public virtual void LevelUp()
        {
            Level++;
            if (this is IValues)
            {
                Cooldown.Duration = (int)(this as IValues).Values["cooldown"][Level];
            }
            
        }
        public virtual bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender is not PlayerCommandSender)
            {
                response = "You cannot use this ability in console.";

                return false;
            }

            if (Player.Get(sender) != Owner)
            {
                response = "You havent this ability.";

                return false;
            }

            response = $"Ability {Name} was used.";

            if (!Cooldown.IsCompleted)
            {
                return false;
            }

            if (this is IToggleAbility)
            {
                (this as IToggleAbility).IsActive = !(this as IToggleAbility).IsActive;
            }

            Cooldown.Run();

            return true;
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
