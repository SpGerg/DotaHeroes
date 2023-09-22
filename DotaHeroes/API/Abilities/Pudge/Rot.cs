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
    public class Rot : ToggleAbility, ICastRange, IValues
    {
        public override string Name => "rot";

        public override string Description => "A toxic cloud that deals intense damage and slows movement--harming not only enemy units but Pudge himself.";

        public override string Lore => "A foul odor precedes a toxic, choking gas, emanating from the Butcher's putrid, ever-swelling mass.";

        public override AbilityType AbilityType => AbilityType.Toggle;

        public override TargetType TargetType => TargetType.None;

        public IReadOnlyDictionary<string, List<float>> Values => new Dictionary<string, List<float>>()
        {
            { "damage", new List<float> { 1, 1, 1, 1 } },
            { "mana_cost", new List<float> { 0, 0, 0, 0 } },
            { "cast_range", new List<float> { 1.5f, 2, 4, 5 } },
        };

        public override int MaxLevel => 4;

        public int Range { get; set; } = 5;

        public override bool IsActive { get; set; }


        public Rot() : base()
        {
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

        public override bool Deactivate(Hero hero, ArraySegment<string> arguments, out string response)
        {
            hero.Values["is_rot"] = false;

            response = "Rot is disabled";

            return true;
        }

        private IEnumerator<float> RotCoroutine(Hero owner)
        {
            while ((bool)owner.Values["is_rot"])
            {
                foreach (var player in Player.List)
                {
                    var hero = API.GetHeroOrDefault(player.UserId);

                    if (hero == default)
                    {
                        continue;
                    }

                    if (Vector3.Distance(owner.Player.Transform.position, player.Transform.position) < Values["cast_range"][Level])
                    {
                        hero.EnableEffect(new Effects.Pudge.Rot(player));
                        hero.GetEffectOrDefault<Effects.Pudge.Rot>().DamageOverTime.Damage = (int)Values["damage"][Level];
                    }
                    else
                    {
                        hero.DisableEffect<Effects.Pudge.Rot>();
                    }
                }

                yield return Timing.WaitForSeconds(0.5f);
            }

            foreach (var player in Player.List)
            {
                var hero = API.GetHeroOrDefault(player.UserId);

                if (hero == default)
                {
                    continue;
                }

                hero.DisableEffect<Effects.Pudge.Rot>();
            }
        }
    }
}
