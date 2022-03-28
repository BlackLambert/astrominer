using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class SelectionSpriteIndicator : SelectionIndicator, Injectable
	{
		[SerializeField]
		private SpriteRenderer _image;

		protected override void SetColor(Color color)
		{
			_image.color = color;
		}
	}
}
