using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Astrominer
{
    public class SceneObjectFinder
    {
		// Source: https://answers.unity.com/questions/863509/how-can-i-find-all-objects-that-have-a-script-that.html
		public static T FindSingleInstance<T>()
		{
			IEnumerable<T> searchResult = GetInstancesOf<T>();
			if (searchResult.Count() == 0)
				throw new NoInstanceFoundException();
			if (searchResult.Count() > 1)
				throw new TooManyInstancesExceptions();
			return searchResult.First();

			static IEnumerable<S> GetInstancesOf<S>()
			{
				List<S> result = new List<S>();
				GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
				foreach (var rootGameObject in rootGameObjects)
				{
					S[] childrenInterfaces = rootGameObject.GetComponentsInChildren<S>();
					result.AddRange(childrenInterfaces);
				}
				return result;
			}
		}

		public class NoInstanceFoundException : Exception { }
        public class TooManyInstancesExceptions : Exception { }
    }
}