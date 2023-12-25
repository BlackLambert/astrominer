using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PlaceBaseOnClick : MonoBehaviour, Injectable
    {
        private BasePlacementPreview _basePlacementPreview;
        private BasePlacementContext _basePlacementContext;
        private PointerInput _pointerInput;
        private Player _player;
        private BasePositions _positions;

        public void Inject(Resolver resolver)
        {
            _basePlacementPreview = resolver.Resolve<BasePlacementPreview>();
            _basePlacementContext = resolver.Resolve<BasePlacementContext>();
            _pointerInput = resolver.Resolve<PointerInput>(0);
            _player = resolver.Resolve<Player>();
            _positions = resolver.Resolve<BasePositions>();
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
            _positions.Add(_player, _basePlacementPreview.transform.position);
        }
    }
}