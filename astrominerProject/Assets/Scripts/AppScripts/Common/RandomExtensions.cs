using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace SBaier.Astrominer
{
    public static class RandomExtensions
    {
        public static Random CreateWithNewSeed(this Random random)
        {
            return new Random((int)(random.NextDouble() * int.MaxValue));
        }
    }
}
