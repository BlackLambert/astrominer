using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class SelectionIndicator : MonoBehaviour, Injectable
	{
		[SerializeField]
		private SpriteRenderer _image;

		private VisualsSettings _visualSettings;

		public void Inject(Resolver resolver)
		{
			_visualSettings = resolver.Resolve<VisualsSettings>();
		}

		private void Start()
		{
			_image.color = _visualSettings.SelectIndicatorColor;
		}
	}
}
