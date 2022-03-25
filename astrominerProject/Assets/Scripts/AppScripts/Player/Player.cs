using UnityEngine;

namespace SBaier.Astrominer
{
    public class Player
    {
        public Color Color;
        public IdentifiedAsteroids IdentifiedAsteroids { get; private set; } = new IdentifiedAsteroids();
        public ProspectorDrones ProspectorDrones { get; private set; } = new ProspectorDrones();
        public Currency Credits { get; private set; } = new Currency();

        public Player(Color color)
		{
            Color = color;
        }
    }
}
