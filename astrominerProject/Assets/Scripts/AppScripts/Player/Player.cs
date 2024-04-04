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
        public OwnedAsteroids OwnedAsteroids { get; private set; } = new OwnedAsteroids();
        public Drones Drones { get; private set; } = new Drones();
        public Currency Credits { get; private set; } = new Currency();
        public Observable<Ship> Ship { get; private set; } = new Observable<Ship>();
        public Observable<float> TotalValue { get; } = 0;
        public ObservableList<float> ValueHistory { get; private set; } = new ObservableList<float>();

        public string GetDisplayText()
        {
            return $"{Name} ({GetPlayerType()})";
        }
        
        public string GetColoredDisplayText()
        {
            return $"<color=#{ColorUtility.ToHtmlStringRGB(Color)}>{GetColoredName()}</color> ({GetPlayerType()})";
        }

        public void AddPlayerValue(float value)
        {
            TotalValue.Value = value;
            ValueHistory.Add(value);
        }

        public void ResetPlayerValue()
        {
            TotalValue.Value = 0;
            ValueHistory.Clear();
        }

        private string GetPlayerType()
        {
            return IsHuman ? "Human" : "Computer";
        }

        private string GetColoredName()
        {
            return $"<color=#{ColorUtility.ToHtmlStringRGB(Color)}>{Name}</color>";
        }

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
