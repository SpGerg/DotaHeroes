using DotaHeroes.API.Enums;
using DotaHeroes.API.Features.Components;
using DotaHeroes.API.Interfaces;
using MEC;
using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEngine.GraphicsBuffer;
using UnityEngine;

namespace DotaHeroes.API.Features.Objects
{
    public class RotDecorateObject : NetworkBehaviour
    {
        public Hero Owner { get; private set; }

        public void Initialize(Hero owner)
        {
            Owner = owner;
        }

        public void Update()
        {
            transform.position = Owner.Player.Position;
        }
    }
}
