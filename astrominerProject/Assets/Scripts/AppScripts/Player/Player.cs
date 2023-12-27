using System;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class Player
    {
        public Guid ID { get; }
        public Color Color { get; }
        public string Name { get; }
        public bool IsHuman { get; }
        public IdentifiedAsteroids IdentifiedAsteroids { get; private set; } = new IdentifiedAsteroids();
        public ProspectorDrones ProspectorDrones { get; private set; } = new ProspectorDrones();
        public Currency Credits { get; private set; } = new Currency();

        public Player(Guid iD,
            Color color,
            string name,
            bool isHuman)
		{
            ID = iD;
            Color = color;
            Name = name;
            IsHuman = isHuman;
        }
    }
}
