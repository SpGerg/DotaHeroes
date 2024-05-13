using DotaHeroes.API;
using DotaHeroes.API.Abilities.Items;
using DotaHeroes.API.Abilities.Pudge;
using DotaHeroes.API.Abilities.SpiritBreaker;
using DotaHeroes.API.Auras.Items;
using DotaHeroes.API.Heroes;
using DotaHeroes.API.Items;
using DotaHeroes.API.Items.Recipes;
using DotaHeroes.Events.Internal;
using Exiled.API.Features;
using System;
using System.IO;
using Hero = DotaHeroes.API.Events.Handlers.Hero;
using Player = Exiled.Events.Handlers.Player;

namespace DotaHeroes
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin Instance { get; private set; }

        public override Version Version => new(1, 1, 1);

        public override Version RequiredExiledVersion => new(8, 8, 0);

        public override string Author => "SpGerg";

        public string SoundsPath { get; private set; }

        public override void OnEnabled()
        {
            Instance = this;
            SoundsPath = Instance.ConfigPath.Replace("\\7777.yml", string.Empty).Replace($"\\{Server.Port}.yml", string.Empty);

            Log.Info("===========================================");
            Log.Info("        Thanks for using DotaHeroes        ");
            Log.Info("               by SpGerg                   ");
            Log.Info($"           Version  {Version}             ");
            Log.Info("===========================================");
            Log.Info("If you found eror in plugin, dm me in discord (#spgerg)");

            //Here abilities
            DTAPI.RegisterAbility(new MeatHook(null));
            DTAPI.RegisterAbility(new Rot(null));
            DTAPI.RegisterAbility(new FleshHeap(null));
            DTAPI.RegisterAbility(new Dismember(null));
            DTAPI.RegisterAbility(new ChargeOfDarkness(null));
            DTAPI.RegisterAbility(new Bulldoze(null));
            DTAPI.RegisterAbility(new GreaterBash(null));
            DTAPI.RegisterAbility(new NetherStrike(null));
            DTAPI.RegisterAbility(new UnholyStrength(null));
            DTAPI.RegisterAbility(new SwitchAttribute(null));
            DTAPI.RegisterAbility(new Phase(null));
            DTAPI.RegisterAbility(new CrystalysCriticalStrike(null));
            DTAPI.RegisterAbility(new BucklerAura(null));

            //Here heroes
            DTAPI.RegisterHero(new Pudge());
            DTAPI.RegisterHero(new SpiritBreaker());

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
