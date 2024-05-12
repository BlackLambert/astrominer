using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class AsteroidQualityDisplay : ItemPropertyDisplay<Asteroid>, Cleanable
	{
		[SerializeField]
		private string _baseString = "Quality: {0}";

		private IdentifiedAsteroids _asteroids;

		public override void Inject(Resolver resolver)
		{
			base.Inject(resolver);
			_asteroids = resolver.Resolve<IdentifiedAsteroids>();
		}

		public override void Initialize()
		{
			base.Initialize();
			_asteroids.OnItemAdded += OnAsteroidAdded;
		}

		public void Clean()
		{
			_asteroids.OnItemAdded -= OnAsteroidAdded;
		}

		protected override string GetText()
		{
			string value = _asteroids.Contains(_item) ? _item.Quality.ToString() : "?";
			return string.Format(_baseString, value);
		}

		private void OnAsteroidAdded(Asteroid asteroid)
		{
			if (asteroid == _item)
				SetText();
		}
	}
}
