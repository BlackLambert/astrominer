using SBaier.DI;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
	public class SelectionImageIndicator : SelectionIndicator, Injectable
	{
		[SerializeField]
		private Image _image;

		protected override void SetColor(Color color)
		{
			_image.color = color;
		}
	}
}
