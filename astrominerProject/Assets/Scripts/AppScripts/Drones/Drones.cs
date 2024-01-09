using System.Linq;

namespace SBaier.Astrominer
{
    public class Drones : ObservableList<Drone> 
    { 
        public bool ContainsDroneTo<TDrone>(Asteroid asteroid) where TDrone : Drone
		{
            return _items.AsReadOnly().Any(drone => drone.Target == asteroid && drone.GetType() == typeof(TDrone));
		}
    }
}
