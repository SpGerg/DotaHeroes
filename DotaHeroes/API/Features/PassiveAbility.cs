namespace DotaHeroes.API.Features
{
    public abstract class PassiveAbility : Ability
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PassiveAbility" /> class.
        /// </summary>
        public PassiveAbility() : base()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PassiveAbility" /> class.
        /// </summary>
        /// <param name="hero"><inheritdoc cref="Hero" /></param>
        public PassiveAbility(Hero hero) : base(hero)
        {
        }

        public abstract void Register();

        public abstract void Unregister();
    }
}
