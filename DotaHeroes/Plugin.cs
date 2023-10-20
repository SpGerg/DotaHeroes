using DotaHeroes.Events.Internal;
using Exiled.API.Features;
using Player = Exiled.Events.Handlers.Player;
using Hero = DotaHeroes.API.Events.Handlers.Hero;
using DotaHeroes.API.Heroes;
using DotaHeroes.API.Abilities.Pudge;
using DotaHeroes.API.Abilities.SpiritBreaker;
using DotaHeroes.API.Items;
using System.IO;
using DotaHeroes.API.Items.Recipes;
using DotaHeroes.API.Abilities.Items;

namespace DotaHeroes
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin Instance { get; private set; }

        public string SoundsPath { get; private set; } 

        public override void OnEnabled()
        {
            Instance = this;
            SoundsPath = Instance.ConfigPath.Replace("\\7777.yml", string.Empty).Replace($"\\{Server.Port}.yml", string.Empty);

            API.API.RegisterAbility(new MeatHook());
            API.API.RegisterAbility(new Rot());
            API.API.RegisterAbility(new FleshHeap());
            API.API.RegisterAbility(new Dismember());
            API.API.RegisterAbility(new ChargeOfDarkness());
            API.API.RegisterAbility(new Bulldoze());
            API.API.RegisterAbility(new GreaterBash());
            API.API.RegisterAbility(new NetherStrike());
            API.API.RegisterAbility(new UnholyStrength());

            if (Config.Heroes["pudge"].IsRegistering)
            {
                API.API.RegisterHero(new Pudge());
            }

            if (Config.Heroes["spirit_breaker"].IsRegistering)
            {
                API.API.RegisterHero(new SpiritBreaker());
            }

            API.API.RegisterItem(new Bracer());
            API.API.RegisterItem(new WraithBand());
            API.API.RegisterItem(new NullTalisman());
            API.API.RegisterItem(new MantleOfIntelligence());
            API.API.RegisterItem(new SlippersOfAgility());
            API.API.RegisterItem(new GauntletsOfStrength());
            API.API.RegisterItem(new Circlet());
            API.API.RegisterItem(new BracerRecipe());
            API.API.RegisterItem(new WraithBandRecipe());
            API.API.RegisterItem(new NullTalismanRecipe());
            API.API.RegisterItem(new HelmOfIronWill());
            API.API.RegisterItem(new BladesOfAttack());
            API.API.RegisterItem(new GlovesOfHaste());
            API.API.RegisterItem(new ArmletOfMordiggianRecipe());
            API.API.RegisterItem(new ArmletOfMordiggian());

            Player.ChangingRole += PlayerHandler.SetHero;
            Player.Left += PlayerHandler.RemoveHero;
            Hero.Died += HeroHandler.GiveExperienceAndMoneyFromKill;
            Hero.TakingDamage += HeroHandler.BlockingDamage;
            Hero.TakedDamage += HeroHandler.UpdateHudOnTakedDamage;
            Hero.ExecutingAbility += HeroHandler.Silence;
            Hero.Died += HeroHandler.AddFleshHeapStackOnDied;
            Hero.Healed += HeroHandler.UpdateHudOnHealed;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Player.ChangingRole -= PlayerHandler.SetHero;
            Player.Left -= PlayerHandler.RemoveHero;
            Hero.Died -= HeroHandler.GiveExperienceAndMoneyFromKill;
            Hero.TakingDamage -= HeroHandler.BlockingDamage;
            Hero.TakedDamage -= HeroHandler.UpdateHudOnTakedDamage;
            Hero.ExecutingAbility -= HeroHandler.Silence;
            Hero.Died -= HeroHandler.AddFleshHeapStackOnDied;
            Hero.Healed -= HeroHandler.UpdateHudOnHealed;

            base.OnDisabled();
        }
    }
}
