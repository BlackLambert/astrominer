using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Astrominer
{
    public class SceneObjectFinder
    {
        public static T FindSingleInstance<T>()
		{
            IEnumerable<T> searchResult = GameObject.FindObjectsOfType<MonoBehaviour>().OfType<T>();
            if (searchResult.Count() == 0)
                throw new NoInstanceFoundException();
            if (searchResult.Count() > 1)
                throw new TooManyInstancesExceptions();
            return searchResult.First();
        }

        public class NoInstanceFoundException : Exception { }
        public class TooManyInstancesExceptions : Exception { }
    }
}