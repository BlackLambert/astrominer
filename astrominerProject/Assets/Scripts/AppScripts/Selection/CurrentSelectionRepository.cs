using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Astrominer
{
	public interface CurrentSelectionRepository
	{
		Selectable CurrentSelection { get; }
		bool IsEmpty { get; }
       
		void Select(Selectable selectable);
		void Deselect();
		bool IsSelected(Selectable selectable);
	}
}