using DotaHeroes.API.Features;
using DotaHeroes.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Modifiers
{
    public class AccuracyModifier : IAccuracyModifier
    {
        public decimal Accuracy { get; set; }

        public AccuracyModifier(decimal accuracy)
        {
            Accuracy = accuracy;
        }
    }
}
