using SBaier.DI;
using System;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class PlayerFactory : Factory<Player, PlayerFactory.Arguments>, Injectable
	{
		private PlayerSettings _settings;

		public void Inject(Resolver resolver)
		{
			_settings = resolver.Resolve<PlayerSettings>();
		}

		public Player Create(Arguments arguments)
		{
			Player result = new Player(arguments.Number, arguments.Color, arguments.Name, arguments.IsHuman, _settings.StartCredits);
			return result;
		}

		public struct Arguments
        {
	        public int Number { get; }
			public Color Color { get; }
			public string Name { get; }
			public bool IsHuman { get; }

			public Arguments(int number, Color color, string name, bool isHuman)
			{
				Number = number;
				Color = color;
				Name = name;
				IsHuman = isHuman;
            }
        }
	}
}
