﻿using DotaHeroes.API.Interfaces;

namespace DotaHeroes.API.Modifiers
{
    public class AccuracyModifier : IAccuracyModifier
    {
        public double Accuracy { get; set; }

        public AccuracyModifier(double accuracy)
        {
            Accuracy = accuracy;
        }
    }
}
