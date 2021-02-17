using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Astrominer
{
	public class CurrentSelectionRepository
	{
		public Selectable CurrentSelection { get; private set; } = null;
		public bool IsEmpty => CurrentSelection is null;

		public void Select(Selectable selectable)
		{
			if (selectable is null)
				throw new ArgumentNullException();
			if (CurrentSelection == selectable)
				throw new AlreadySelectedException();
			CurrentSelection?.Deselect();
			CurrentSelection = selectable;
			CurrentSelection.Select();
		}

		public void Deselect()
		{
			if (IsEmpty)
				throw new CurrentSelectionIsNullException();
			CurrentSelection.Deselect();
			CurrentSelection = null;
		}

		public class CurrentSelectionIsNullException : InvalidOperationException
		{
		}

		public class AlreadySelectedException : InvalidOperationException
		{
		}
	}
}