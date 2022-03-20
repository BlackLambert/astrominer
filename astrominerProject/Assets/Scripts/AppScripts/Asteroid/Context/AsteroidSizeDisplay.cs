using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class AsteroidSizeDisplay : ItemPropertyDisplay<Asteroid>
	{
		[SerializeField]
		private string _baseString = "Size: {0}";

		private Player _player;

		public override void Inject(Resolver resolver)
		{
			base.Inject(resolver);
			_player = resolver.Resolve<Player>();
		}

		protected override void Start()
		{
			base.Start();
			_player.OnIdentifiedAsteroidAdded += OnAsteroidAdded;
		}

		private void OnDestroy()
		{
			_player.OnIdentifiedAsteroidAdded -= OnAsteroidAdded;
		}

		protected override string GetText()
		{
			string value = _player.IsIdentified(_item) ? _item.Size.ToString() : "?";
			return string.Format(_baseString, value);
		}

		private void OnAsteroidAdded(Asteroid asteroid)
		{
			if (asteroid == _item)
				SetText();
		}
	}
}
