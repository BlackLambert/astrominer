using UnityEngine;

namespace SBaier.Astrominer
{
    public class Player
    {
        public Color Color;
        public IdentifiedAsteroids IdentifiedAsteroids { get; set; } = new IdentifiedAsteroids();
        public ProspectorDrones ProspectorDrones { get; set; } = new ProspectorDrones();

        public Player(Color color)
		{
            Color = color;
        }
    }
}
