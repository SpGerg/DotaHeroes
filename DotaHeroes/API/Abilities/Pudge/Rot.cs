using DotaHeroes.API.Abilities.Items;
using DotaHeroes.API.Enums;
using DotaHeroes.API.Features;
using DotaHeroes.API.Features.Objects;
using DotaHeroes.API.Interfaces;
using Exiled.API.Features;
using Exiled.API.Features.Toys;
using MEC;
using Mirror;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DotaHeroes.API.Abilities.Pudge
{
    public class Rot : ToggleAbility, ILevelValues
    {
        public override string Name => "Rot";

        public override string Slug => "rot";

        public override string Description => "A toxic cloud that deals intense damage and slows movement--harming not only enemy units but Pudge himself.";

        public override string Lore => "A foul odor precedes a toxic, choking gas, emanating from the Butcher's putrid, ever-swelling mass.";

        public override AbilityType AbilityType => AbilityType.Toggle;

        public override TargetType TargetType => TargetType.None;

        public Dictionary<string, List<decimal>> Values { get; } = Plugin.Instance.Config.Abilites["rot"].Values;

        public int MaxLevel { get; set; } = 4;

        public int MinLevel { get; set; } = 0;

        public IReadOnlyList<int> HeroLevelToLevelUp { get; set; } = Features.Utils.EmptyLevelsList;

        public static string SoundsPath = Plugin.Instance.SoundsPath + "\\pudge\\rot";

        public Rot() : base() { }

        public Rot(Hero hero) : base(hero) { }

        public override bool Activate(ArraySegment<string> arguments, out string response)
        {
            if (!Owner.Values.ContainsKey("is_rot"))
            {
                Owner.Values.Add("is_rot", true);
            }
            else
            {
                Owner.Values["is_rot"] = true;
            }

            var decorateRot = Primitive.Create(Owner.Player.Position, Quaternion.identity.eulerAngles, -Vector3.one, true);
            decorateRot.Type = PrimitiveType.Cube;
            decorateRot.MovementSmoothing = 60;
            decorateRot.Color = Color.yellow;
            decorateRot.AdminToyBase.gameObject.AddComponent<RotDecorateObject>().Initialize(Owner);

            Owner.Values["decorate_rot"] = decorateRot;
            Owner.Values["audio_rot"] = Audio.Play(Owner.Player.Position, SoundsPath + "\\rot.ogg", 100f, true, Owner.Player);

            Timing.RunCoroutine(RotCoroutine());

            response = "Rot is enabled";
            return true;
        }

        public override bool Deactivate(ArraySegment<string> arguments, out string response)
        {
            Owner.Values["is_rot"] = false;
            try
            {
                NetworkServer.Destroy((Owner.Values["decorate_rot"] as Primitive).AdminToyBase.gameObject);
            }
            catch { }

            try
            {
                Audio.StopLoop(Owner.Values["audio_rot"] as Player);
            }
            catch { }

            response = "Rot is disabled";
            return true;
        }

        private IEnumerator<float> RotCoroutine()
        {
            while ((bool)Owner.Values["is_rot"] && !Owner.IsHeroDead)
            {
                foreach (var hero in DTAPI.GetHeroes().Values)
                {
                    if (Vector3.Distance(Owner.Player.Position, Owner.Player.Position) < 2)
                    {
                        var rot = new Effects.Pudge.Rot(hero);
                        rot.Damage = (int)Values["damage"][Level];
                        rot.DamageType = DamageType.Magical;
                        rot.Attacker = Owner;
                        hero.EnableEffect(rot);
                    }
                    else
                    {
                        hero.DisableEffect<Effects.Pudge.Rot>();
                    }
                }

                yield return Timing.WaitForSeconds(1);
            }

            foreach (var hero in DTAPI.GetHeroes().Values)
            {
                hero.DisableEffect<Effects.Pudge.Rot>();
            }
        }

        public override Ability Create(Hero hero)
        {
            return new Rot(hero);
        }
    }
}
