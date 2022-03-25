using SBaier.DI;

namespace SBaier.Astrominer
{
	public class PlayerFactory : Factory<Player>, Injectable
	{
		private PlayerSettings _settings;

		public void Inject(Resolver resolver)
		{
			_settings = resolver.Resolve<PlayerSettings>();
		}

		public Player Create()
		{
			Player result = new Player(_settings.PlayerColor);
			result.Credits.Add(_settings.StartCredits);
			return result;
		}
	}
}
