using System.Collections.Generic;
using SBaier.DI;

namespace SBaier.Astrominer
{
    public class AIActionsFactory : Factory<List<AIAction>, Player>, Injectable
    {
        private Factory<FlyToRandomAsteroidAction, Player> _randomAsteroidActionFactory;
        
        public void Inject(Resolver resolver)
        {
            _randomAsteroidActionFactory = resolver.Resolve<Factory<FlyToRandomAsteroidAction, Player>>();
        }

        public List<AIAction> Create(Player player)
        {
            List<AIAction> result = new List<AIAction>();
            result.Add(_randomAsteroidActionFactory.Create(player));
            return result;
        }
    }
}
