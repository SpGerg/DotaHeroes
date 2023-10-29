using DotaHeroes.Events.Internal;
using Exiled.API.Features;
using Player = Exiled.Events.Handlers.Player;
using Hero = DotaHeroes.API.Events.Handlers.Hero;
using DotaHeroes.API.Heroes;
using DotaHeroes.API.Abilities.Pudge;
using DotaHeroes.API.Abilities.SpiritBreaker;
using DotaHeroes.API.Items;
using DotaHeroes.API.Items.Recipes;
using DotaHeroes.API.Abilities.Items;
using DotaHeroes.API;
using System;
using DotaHeroes.API.Auras.Items;

namespace DotaHeroes
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin Instance { get; private set; }

        public override Version Version => new Version(1, 1, 0);

        public override Version RequiredExiledVersion => new Version(8, 2, 1);

        public override string Author => "SpGerg";

        public string SoundsPath { get; private set; } 

        public override void OnEnabled()
        {
            Instance = this;
            SoundsPath = Instance.ConfigPath.Replace("\\7777.yml", string.Empty).Replace($"\\{Server.Port}.yml", string.Empty);

            DTAPI.RegisterAbility(new MeatHook());
            DTAPI.RegisterAbility(new Rot());
            DTAPI.RegisterAbility(new FleshHeap());
            DTAPI.RegisterAbility(new Dismember());
            DTAPI.RegisterAbility(new ChargeOfDarkness());
            DTAPI.RegisterAbility(new Bulldoze());
            DTAPI.RegisterAbility(new GreaterBash());
            DTAPI.RegisterAbility(new NetherStrike());
            DTAPI.RegisterAbility(new UnholyStrength());
            DTAPI.RegisterAbility(new SwitchAttribute());
            DTAPI.RegisterAbility(new Phase());
            DTAPI.RegisterAbility(new CrystalysCriticalStrike());
            DTAPI.RegisterAbility(new BucklerAura());

            if (Config.Heroes["pudge"].IsRegistering)
            {
                DTAPI.RegisterHero(new Pudge());
            }

            if (Config.Heroes["spirit_breaker"].IsRegistering)
            {
                DTAPI.RegisterHero(new SpiritBreaker());
            }

            //Here ingredients
            DTAPI.RegisterItem(new Circlet());
            DTAPI.RegisterItem(new Broadsword());
            DTAPI.RegisterItem(new RingOfHealth());
            DTAPI.RegisterItem(new VitalityBooster());
            DTAPI.RegisterItem(new BootsOfSpeed());
            DTAPI.RegisterItem(new Chainmail());
            DTAPI.RegisterItem(new MantleOfIntelligence());
            DTAPI.RegisterItem(new SlippersOfAgility());
            DTAPI.RegisterItem(new GauntletsOfStrength());
            DTAPI.RegisterItem(new HelmOfIronWill());
            DTAPI.RegisterItem(new BladesOfAttack());
            DTAPI.RegisterItem(new GlovesOfHaste());
            DTAPI.RegisterItem(new RingOfProtection());
            DTAPI.RegisterItem(new ArmletOfMordiggianRecipe());
            DTAPI.RegisterItem(new WraithBandRecipe());
            DTAPI.RegisterItem(new NullTalismanRecipe());
            DTAPI.RegisterItem(new BracerRecipe());
            DTAPI.RegisterItem(new CrystalysRecipe());
            DTAPI.RegisterItem(new BucklerRecipe());

            //Here items from ingredients
            DTAPI.RegisterItem(new Bracer());
            DTAPI.RegisterItem(new WraithBand());
            DTAPI.RegisterItem(new NullTalisman());
            DTAPI.RegisterItem(new ArmletOfMordiggian());
            DTAPI.RegisterItem(new PowerTreads());
            DTAPI.RegisterItem(new PhaseBoots());
            DTAPI.RegisterItem(new Vanguard());
            DTAPI.RegisterItem(new Crystalys());
            DTAPI.RegisterItem(new Buckler());

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
