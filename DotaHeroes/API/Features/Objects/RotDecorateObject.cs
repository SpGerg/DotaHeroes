using DotaHeroes.API.Enums;
using DotaHeroes.API.Features.Components;
using DotaHeroes.API.Interfaces;
using Mirror;

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
