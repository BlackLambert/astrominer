using UnityEngine;
using TMPro;
using SBaier.DI;

namespace SBaier.Astrominer
{
    public class AsteroidNameDisplay : MonoBehaviour, Injectable
    {
        [SerializeField]
        private TextMeshProUGUI _text;

		private Asteroid _asteroid;

		public void Inject(Resolver resolver)
		{
			_asteroid = resolver.Resolve<Asteroid>();
		}

		private void Start()
		{
			_text.text = _asteroid.name;
		}
	}
}
