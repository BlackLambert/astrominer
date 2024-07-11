using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class FlyTargetComparer : IEqualityComparer<FlyTarget>
    {
        public bool Equals(FlyTarget x, FlyTarget y)
        {
            // Check for null on both sides
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            // Check the Id values
            return x.Id == y.Id;
        }

        public int GetHashCode(FlyTarget obj)
        {
            // Use the Id's hash code
            return obj.Id.GetHashCode();
        }
    }
}
