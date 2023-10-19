using DotaHeroes.API.Events.EventArgs;
using DotaHeroes.API.Events.EventArgs.Hero;
using Exiled.Events.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Events.Handlers
{
    public static class Hero
    {
        public static Event<HeroAttackingEventArgs> Attacking { get; set; } = new();

        public static Event<HeroAttackedEventArgs> Attacked { get; set; } = new();

        public static Event<HeroTakingDamageEventArgs> TakingDamage { get; set; } = new();

        public static Event<HeroTakedDamageEventArgs> TakedDamage { get; set; } = new();

        public static Event<HeroDyingEventArgs> Dying { get; set; } = new();

        public static Event<HeroDiedEventArgs> Died { get; set; } = new();

        public static Event<HeroHealingEventArgs> Healing { get; set; } = new();

        public static Event<HeroHealedEventArgs> Healed { get; set; } = new();

        public static Event<HeroRespawningEventArgs> Respawning { get; set; } = new();

        public static Event<HeroRespawnedEventArgs> Respawned { get; set; } = new();

        public static Event<HeroReceivingEffectEventArgs> ReceivingEffect { get; set; } = new();

        public static Event<HeroDisabledEffectEventArgs> Disabled { get; set; } = new();

        public static Event<HeroDispellingEventArgs> Dispelling { get; set; } = new();

        public static Event<HeroDispelledEventArgs> Dispelled { get; set; } = new();

        public static Event<HeroExecutingAbilityEventArgs> ExecutingAbility { get; set; } = new();

        public static Event<HeroExecutedAbilityEventArgs> ExecutedAbility { get; set; } = new();

        public static void OnHeroAttacking(HeroAttackingEventArgs ev) => Attacking.InvokeSafely(ev);

        public static void OnHeroAttacked(HeroAttackedEventArgs ev) => Attacked.InvokeSafely(ev);

        public static void OnHeroTakingDamage(HeroTakingDamageEventArgs ev) => TakingDamage.InvokeSafely(ev);

        public static void OnHeroTakedDamage(HeroTakedDamageEventArgs ev) => TakedDamage.InvokeSafely(ev);

        public static void OnHeroDying(HeroDyingEventArgs ev) => Dying.InvokeSafely(ev);

        public static void OnHeroDied(HeroDiedEventArgs ev) => Died.InvokeSafely(ev);

        public static void OnHeroHealing(HeroHealingEventArgs ev) => Healing.InvokeSafely(ev);

        public static void OnHeroHealed(HeroHealedEventArgs ev) => Healed.InvokeSafely(ev);

        public static void OnHeroRespawning(HeroRespawningEventArgs ev) => Respawning.InvokeSafely(ev);

        public static void OnHeroRespawned(HeroRespawnedEventArgs ev) => Respawned.InvokeSafely(ev);

        public static void OnHeroReceivingEffect(HeroReceivingEffectEventArgs ev) => ReceivingEffect.InvokeSafely(ev);

        public static void OnHeroDisabledEffect(HeroDisabledEffectEventArgs ev) => Disabled.InvokeSafely(ev);

        public static void OnHeroDispelling(HeroDispellingEventArgs ev) => Dispelling.InvokeSafely(ev);

        public static void OnHeroDispelled(HeroDispelledEventArgs ev) => Dispelled.InvokeSafely(ev);

        public static void OnHeroExecutingAbility(HeroExecutingAbilityEventArgs ev) => ExecutingAbility.InvokeSafely(ev);

        public static void OnHeroExecutedAbility(HeroExecutedAbilityEventArgs ev) => ExecutedAbility.InvokeSafely(ev);
    }
}
