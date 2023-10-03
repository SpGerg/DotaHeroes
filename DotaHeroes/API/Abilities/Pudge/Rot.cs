using CommandSystem;
using DotaHeroes.API.Effects.Pudge;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Events.Handlers;
using DotaHeroes.API.Features;
using DotaHeroes.API.Features.Components;
using DotaHeroes.API.Features.Objects;
using DotaHeroes.API.Interfaces;
using Exiled.API.Features;
using Exiled.API.Features.Toys;
using MEC;
using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Hero = DotaHeroes.API.Features.Hero;

namespace DotaHeroes.API.Abilities.Pudge
{
    [CommandHandler(typeof(ClientCommandHandler))]
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Rot : ToggleAbility, ILevelValues
    {
        public override string Name => "Rot";

        public override string Description => "A toxic cloud that deals intense damage and slows movement--harming not only enemy units but Pudge himself.";

        public override string Lore => "A foul odor precedes a toxic, choking gas, emanating from the Butcher's putrid, ever-swelling mass.";

        public override AbilityType AbilityType => AbilityType.Toggle;

        public override TargetType TargetType => TargetType.None;

        public override bool IsActive { get; set; }

        public Dictionary<string, List<float>> Values => Plugin.Instance.Config.Abilites["rot"].Values;

        public int MaxLevel { get; set; } = 4;

        public int MinLevel { get; set; } = 0;

        public IReadOnlyList<int> HeroLevelToLevelUp { get; set; } = new List<int>();

        public float Damage { get; private set; }

        public Rot() : base()
        {
            Damage = (int)Values["damage"][Level];
        }

        public override bool Activate(Hero hero, ArraySegment<string> arguments, out string response)
        {
            if (!hero.Values.ContainsKey("is_rot"))
            {
                hero.Values.Add("is_rot", true);
            }
            else
            {
                hero.Values["is_rot"] = true;
            }

            Timing.RunCoroutine(RotCoroutine(hero));

            response = "Rot is enabled";
            return true;
        }

        public override void LevelUp(Hero hero)
        {
            Damage = (int)Values["damage"][Level];

            base.LevelUp(hero);
        }

        public override bool Deactivate(Hero hero, ArraySegment<string> arguments, out string response)
        {
            hero.Values["is_rot"] = false;

            response = "Rot is disabled";
            return true;
        }

        private IEnumerator<float> RotCoroutine(Hero owner)
        {
            while ((bool)owner.Values["is_rot"] && !owner.IsHeroDead)
            {
                foreach (var hero in API.GetHeroes().Values)
                {
                    if (Vector3.Distance(hero.Player.Position, owner.Player.Position) < 2)
                    {
                        var rot = new Effects.Pudge.Rot(hero);
                        rot.Damage = (int)Damage;
                        rot.DamageType = DamageType.Magical;
                        hero.EnableEffect(rot);
                    }
                    else
                    {
                        hero.DisableEffect<Effects.Pudge.Rot>();
                    }
                }

                yield return Timing.WaitForSeconds(1);
            }

            foreach (var hero in API.GetHeroes().Values)
            {
                hero.DisableEffect<Effects.Pudge.Rot>();
            }
        }
    }
}
