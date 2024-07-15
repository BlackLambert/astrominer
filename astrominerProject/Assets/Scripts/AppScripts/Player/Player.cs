using System;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class Player
    {
        public int Number { get; }
        public Color Color { get; }
        public string Name { get; }
        public bool IsHuman { get; }
        public IdentifiedAsteroids IdentifiedAsteroids { get; private set; } = new IdentifiedAsteroids();
        public OwnedAsteroids OwnedAsteroids { get; private set; } = new OwnedAsteroids();
        public Drones Drones { get; private set; } = new Drones();
        public Currency Credits { get; private set; }
        public Observable<Ship> Ship { get; private set; } = new Observable<Ship>();
        private float _startCredits;
        
        public Player(
            int number,
            Color color,
            string name,
            bool isHuman,
            float startCredits)
        {
            Number = number;
            Color = color;
            Name = name;
            IsHuman = isHuman;
            Credits = new Currency(startCredits);
            _startCredits = startCredits;
        }

        public void Reset()
        {
            IdentifiedAsteroids.Clear();
            OwnedAsteroids.Clear();
            Drones.Clear();
            Credits.Set(_startCredits);
        }
        
        public string GetDisplayText()
        {
            return $"{Name} ({GetPlayerType()})";
        }
        
        public string GetColoredDisplayText()
        {
            return $"<color=#{ColorUtility.ToHtmlStringRGB(Color)}>{GetColoredName()}</color> ({GetPlayerType()})";
        }

        private string GetPlayerType()
        {
            return IsHuman ? "Human" : "Computer";
        }

        private string GetColoredName()
        {
            return $"<color=#{ColorUtility.ToHtmlStringRGB(Color)}>{Name}</color>";
        }
    }
}
