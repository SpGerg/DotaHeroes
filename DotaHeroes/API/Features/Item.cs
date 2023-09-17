﻿using DotaHeroes.API.Interfaces;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Features
{
    public abstract class Item : IValues
    {
        public string Name { get; }

        public string Description { get; }

        public string Lore { get; }

        public virtual Ability Ability { get; }

        public virtual IReadOnlyDictionary<string, List<float>> Values { get; }

        public Player Owner { get; }

        public virtual void Added() { }

        public virtual void Removed() { }

        public Item(Player owner)
        {
            Owner = owner;
        }
    }
}