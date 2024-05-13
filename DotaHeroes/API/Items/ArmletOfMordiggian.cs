using DotaHeroes.API.Features;

namespace DotaHeroes.API.Items
{
    public class ArmletOfMordiggian : AutoItem
    {
        public override string Name => "Armlet of Mordiggian";

        public override string Slug => "armlet_of_mordiggian";

        public override string Description => "Armlet of Mordiggian";

        public override string Lore => "Armlet of Mordiggian";

        public ArmletOfMordiggian() : base() { }

        protected ArmletOfMordiggian(Hero owner) : base(owner)
        {
        }

        public override Item Create(Hero owner)
        {
            return new ArmletOfMordiggian(owner);
        }

        public override string ToString()
        {
            return $"{Name}: {(MainAbility as ToggleAbility).IsActive}";
        }
    }
}
