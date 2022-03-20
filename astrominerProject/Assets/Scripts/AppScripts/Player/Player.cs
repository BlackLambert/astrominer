using System;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class Player
    {
        public Color Color;
        public event Action<Asteroid> OnIdentifiedAsteroidAdded;
        private HashSet<Asteroid> _identifiedAsteroids = new HashSet<Asteroid>();

        public Player(Color color)
		{
            Color = color;
        }

        public void AddIdentifiedAsteroid(Asteroid asteroid)
		{
            _identifiedAsteroids.Add(asteroid);
            OnIdentifiedAsteroidAdded?.Invoke(asteroid);
        }

        public bool IsIdentified(Asteroid asteroid)
		{
            return _identifiedAsteroids.Contains(asteroid);
        }
    }
}
