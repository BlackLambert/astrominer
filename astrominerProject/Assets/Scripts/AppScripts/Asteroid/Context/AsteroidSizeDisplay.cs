using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class AsteroidSizeDisplay : ItemPropertyDisplay<Asteroid>
	{
		[SerializeField]
		private string _baseString = "Size: {0}";

		private IdentifiedAsteroids _asteroids;

		public override void Inject(Resolver resolver)
		{
			base.Inject(resolver);
			_asteroids = resolver.Resolve<IdentifiedAsteroids>();
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			_asteroids.OnItemAdded += OnAsteroidAdded;
		}

		private void OnDisable()
		{
			_asteroids.OnItemAdded -= OnAsteroidAdded;
		}

		protected override string GetText()
		{
			string value = _asteroids.Contains(_item) ? _item.Size.ToString() : "?";
			return string.Format(_baseString, value);
		}

		private void OnAsteroidAdded(Asteroid asteroid)
		{
			if (asteroid == _item)
				SetText();
		}
	}
}
