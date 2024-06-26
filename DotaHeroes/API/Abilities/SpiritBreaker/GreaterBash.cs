﻿using DotaHeroes.API.Effects;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Events.EventArgs.Hero;
using DotaHeroes.API.Features;
using DotaHeroes.API.Interfaces;
using Exiled.API.Features;
using System.Collections.Generic;

namespace DotaHeroes.API.Abilities.SpiritBreaker
{
    public class GreaterBash : PassiveAbility, ILevelValues
    {
        public override string Name => "Greater bash";

        public override string Slug => "greater_bash";

        public override string Description => "Greater bash";

        public override string Lore => "Greater bash";

        public override AbilityType AbilityType => AbilityType.Passive;

        public override TargetType TargetType => TargetType.ToEnemy;

        public Dictionary<string, List<double>> Values { get; } = Plugin.Instance.Config.Abilites["greater_bash"].Values;

        public int MaxLevel { get; set; } = 4;

        public int MinLevel { get; set; } = 0;

        public IReadOnlyList<int> HeroLevelToLevelUp { get; } = Features.Utils.EmptyLevelsList;

        public static string SoundsPath = Plugin.Instance.SoundsPath + "\\spirit_breaker\\greater_bash";

        public GreaterBash(Hero hero) : base(hero) { }

        public override void Register()
        {
            Events.Handlers.Hero.Attacking += OnAttack;
        }

        public override void Unregister()
        {
            Events.Handlers.Hero.Attacking -= OnAttack;
        }

        private void OnAttack(HeroAttackingEventArgs ev)
        {
            if (!ev.Hero.Abilities.Contains(this)) return;

            var chance = UnityEngine.Random.Range(0, 100);

            if (Values["chance"][Level] < chance || !Cooldowns.GetCooldown(ev.Hero.Player.Id, Slug).IsReady || ev.Hero.TryGetEffect(out Break _)) return;

            Log.Debug("Bash");

            Bash(ev.Target, ev.Hero, ev.Damage);

            Cooldowns.AddCooldown(ev.Hero.Player.Id, new CooldownInfo(Slug, (float)Values["cooldown"][Level]));
        }

        public void Bash(Hero target, Hero attacker, double damage = -1)
        {
            var total_damage = damage;

            if (total_damage == -1)
            {
                total_damage = ((attacker.HeroStatistics.Speed.Speed / 100) * Values["damage_from_speed"][Level]);
            }

            var effect = new Stun(target);

            target.EnableEffect(effect, (float)target.HeroStatistics.Resistance.GetEffectDuration(Values["stun"][Level]));

            target.TakeDamage(attacker, total_damage, DamageType.Magical);

            Features.Audio.Play(Owner.Player.Position, SoundsPath + "\\bash.ogg", 75f, false, Owner.Player);
        }

        public override Ability Create(Hero hero)
        {
            return new GreaterBash(hero);
        }
    }
}
