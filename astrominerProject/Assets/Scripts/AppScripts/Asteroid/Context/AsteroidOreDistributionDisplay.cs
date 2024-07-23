using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class AsteroidOreDistributionDisplay : ItemPropertyDisplay<Asteroid>, Cleanable
    {
        [SerializeField] private string _baseString = "{0}: {1}";

        [SerializeField] private OreType _oreType;

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
            float portion = _item.TotalExploitableOres.GetPortionOf(_oreType) * 100;
            string value = _asteroids.Contains(_item) ? $"{portion:F0}%" : "?";
            return string.Format(_baseString, _oreType, value);
        }

        private void OnAsteroidAdded(Asteroid asteroid)
        {
            if (asteroid == _item)
                SetText();
        }
    }
}