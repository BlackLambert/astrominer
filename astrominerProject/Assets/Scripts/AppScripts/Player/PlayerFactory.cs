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
			Guid iD = Guid.NewGuid();
			Player result = new Player(iD, arguments.Color, arguments.Name);
			result.Credits.Add(_settings.StartCredits);
			return result;
		}

		public struct Arguments
        {
			public Color Color { get; }
			public string Name { get; }

			public Arguments(Color color, string name)
            {
				Color = color;
				Name = name;
            }
        }
	}
}
