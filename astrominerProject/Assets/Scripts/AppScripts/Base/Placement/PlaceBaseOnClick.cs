using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PlaceBaseOnClick : MonoBehaviour, Injectable
    {
        private BasePlacementPreview _basePlacementPreview;
        private BasePlacementContext _basePlacementContext;
        private BasesPlacementContext _basesPlacementContext;
        private PointerInput _pointerInput;
        private Player _player;

        public void Inject(Resolver resolver)
        {
            _basePlacementPreview = resolver.Resolve<BasePlacementPreview>();
            _basePlacementContext = resolver.Resolve<BasePlacementContext>();
            _pointerInput = resolver.Resolve<PointerInput>(0);
            _basesPlacementContext = resolver.Resolve<BasesPlacementContext>();
            _player = resolver.Resolve<Player>();
        }

        private void OnEnable()
        {
            _pointerInput.OnClick += TryPlace;
        }

        private void OnDisable()
        {
            _pointerInput.OnClick -= TryPlace;
        }

        private void TryPlace()
        {
            if (!_basePlacementContext.PlacementIsValid.Value || !_basePlacementContext.Started.Value)
            {
                return;
            }

            _basePlacementContext.Placed.Value = true;
            _basesPlacementContext.AddBasePosition(_player, _basePlacementPreview.transform.position);
        }
    }
}