using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public abstract class SelectionIndicator : MonoBehaviour, Injectable, Initializable
	{
		private VisualsSettings _visualSettings;

		public void Inject(Resolver resolver)
		{
			_visualSettings = resolver.Resolve<VisualsSettings>();
		}

		public void Initialize()
		{
			SetColor(_visualSettings.SelectIndicatorColor);
		}

		protected abstract void SetColor(Color color);
	}
}
