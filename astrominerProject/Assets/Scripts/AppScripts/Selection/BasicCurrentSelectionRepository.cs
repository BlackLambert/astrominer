using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Astrominer
{
	public class BasicCurrentSelectionRepository : CurrentSelectionRepository
	{
		private Selectable _currentSelection = null;
		public override Selectable CurrentSelection => _currentSelection;

        public override void Deselect()
        {
			if (IsEmpty)
				throw new CurrentSelectionIsNullException();
			_currentSelection.Deselect();
			_currentSelection = null;
		}

        public override void Select(Selectable selectable)
		{
			if (selectable is null)
				throw new ArgumentNullException();
			if (_currentSelection == selectable)
				throw new AlreadySelectedException();
			_currentSelection?.Deselect();
			_currentSelection = selectable;
			_currentSelection.Select();
		}

		public class CurrentSelectionIsNullException : InvalidOperationException
		{
		}

		public class AlreadySelectedException : InvalidOperationException
		{
		}
	}

}

