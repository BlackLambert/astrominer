using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Astrominer
{
	public abstract class CurrentSelectionRepository : MonoBehaviour
	{
		public abstract Selectable CurrentSelection { get; }
		public bool IsEmpty => CurrentSelection is null;

        public abstract void Select(Selectable selectable);

		public abstract void Deselect();


	}
}