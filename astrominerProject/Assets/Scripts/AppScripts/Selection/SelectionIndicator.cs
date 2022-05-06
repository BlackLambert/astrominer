using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public abstract class SelectionIndicator : MonoBehaviour, Injectable
	{
		private VisualsSettings _visualSettings;

		public void Inject(Resolver resolver)
		{
			_visualSettings = resolver.Resolve<VisualsSettings>();
		}

		private void OnEnable()
		{
			SetColor(_visualSettings.SelectIndicatorColor);
		}

		protected abstract void SetColor(Color color);
	}
}
