namespace DotaHeroes.API.Features
{
    public abstract class PassiveAbility : Ability
    {
        public Hero Owner { get; private set; }

        public void RegisterOwner(Hero owner)
        {
            Owner = owner;
        }

        public abstract void Register(Hero owner);

        public abstract void Unregister(Hero owner);
    }
}
