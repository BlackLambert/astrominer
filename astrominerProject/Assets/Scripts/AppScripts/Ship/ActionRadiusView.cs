using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class ActionRadiusView : MonoBehaviour, Injectable
	{
		[SerializeField]
		private Transform _radiusTransform;
		[SerializeField]
		private SpriteRenderer _spriteRenderer;

		private ActionRange _actionRange;
		private VisualsSettings _visualSettings;

		public void Inject(Resolver resolver)
		{
			_actionRange = resolver.Resolve<ActionRange>();
			_visualSettings = resolver.Resolve<VisualsSettings>();
		}

		private void OnEnable()
		{
			float diameter = _actionRange.Range * 2;
			_radiusTransform.localScale = new Vector3(diameter, diameter, diameter);
			_spriteRenderer.color = _visualSettings.ActionRadiusColor;
		}
	}
}
