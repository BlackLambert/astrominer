using System;
using UnityEngine;

namespace Astrominer.Test
{
	public class DummyCurrentSelectionRepository : MonoBehaviour, CurrentSelectionRepository
	{
		public Selectable CurrentSelection => throw new NotImplementedException();
		public event Action<Selectable> OnSelect;
		public bool IsEmpty => throw new NotImplementedException();

		public void Deselect()
		{
			throw new NotImplementedException();
		}

		public bool IsSelected(Selectable selectable)
		{
			return true;
		}

		public void Select(Selectable selectable)
		{
			OnSelect?.Invoke(selectable);
		}
	}
}