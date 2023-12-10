using SBaier.DI;
using System.Collections;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class GamePauserOnFlyTargetReached : MonoBehaviour, Injectable
    {
        private Ship _ship;

        public void Inject(Resolver resolver)
        {
            _ship = resolver.Resolve<Ship>();
        }

        private IEnumerator Start()
        {
            yield return null;
            CheckPause();
            _ship.OnFlyTargetChanged += CheckPause;
            _ship.OnFlyTargetReached += CheckPause;
        }

        private void OnDestroy()
        {
            _ship.OnFlyTargetChanged -= CheckPause;
            _ship.OnFlyTargetReached -= CheckPause;
        }

        private void CheckPause()
        {
            bool pause = !_ship.IsFlying;
            Time.timeScale = pause ? 0 : 1;
        }
    }
}
