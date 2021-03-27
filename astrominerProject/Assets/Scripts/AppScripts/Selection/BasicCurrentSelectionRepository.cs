using System;
using UnityEngine;

namespace Astrominer
{
	public class BasicCurrentSelectionRepository : CurrentSelectionRepository
	{
		public Selectable CurrentSelection { get; private set; } = null;

		public bool IsEmpty => CurrentSelection == null;

		public void Deselect()
        {
			if (IsEmpty)
				throw new CurrentSelectionIsNullException();
			CurrentSelection.Deselect();
			CurrentSelection = null;
		}

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
		public bool IsSelected(Selectable selectable)
		{
			if (selectable is null)
				throw new ArgumentNullException();
			return selectable == CurrentSelection;
		}

		public class CurrentSelectionIsNullException : InvalidOperationException
		{
		}

		public class AlreadySelectedException : InvalidOperationException
		{
		}


	}

}

