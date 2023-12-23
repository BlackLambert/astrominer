using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class BasePlacementValidator : MonoBehaviour, Injectable
    {
        private BasePlacementContext _context;
        private AsteroidsInRangeDetector _asteroidsDetector;
        private CollisionDetector2D _collisionDetector;

        public void Inject(Resolver resolver)
        {
            _context = resolver.Resolve<BasePlacementContext>();
            _asteroidsDetector = resolver.Resolve<AsteroidsInRangeDetector>();
            _collisionDetector = resolver.Resolve<CollisionDetector2D>(nameof(Base));
        }

        private void OnEnable()
        {
            CheckIsValid();
            _asteroidsDetector.OnItemCameInRange += OnAsteroidInRange;
            _asteroidsDetector.OnItemCameOutOffRange += OnAsteroidOutOffRange;
            _collisionDetector.OnCollisionEnter += OnBaseCollisionEnter;
            _collisionDetector.OnCollisionExit += OnBaseCollisionExit;
        }

        private void OnDisable()
        {
            _asteroidsDetector.OnItemCameInRange -= OnAsteroidInRange;
            _asteroidsDetector.OnItemCameOutOffRange -= OnAsteroidOutOffRange;
            _collisionDetector.OnCollisionEnter -= OnBaseCollisionEnter;
            _collisionDetector.OnCollisionExit -= OnBaseCollisionExit;
        }

        private void OnAsteroidInRange(Asteroid obj)
        {
            CheckIsValid();
        }

        private void OnAsteroidOutOffRange(Asteroid obj)
        {
            CheckIsValid();
        }

        private void OnBaseCollisionEnter(Collider2D obj)
        {
            CheckIsValid();
        }

        private void OnBaseCollisionExit(Collider2D obj)
        {
            CheckIsValid();
        }

        private void CheckIsValid()
        {
            bool valid = _asteroidsDetector.ItemsInRange.Count > 0 &&
                         _collisionDetector.Collisions.Count == 0;
            
            if (_context.PlacementIsValid.Value != valid)
            {
                _context.PlacementIsValid.Value = valid;
            }
        }
    }
}