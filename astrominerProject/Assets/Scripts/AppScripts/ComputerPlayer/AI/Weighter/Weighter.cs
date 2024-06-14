using SBaier.AI;

namespace SBaier.Astrominer
{
    public abstract class Weighter
    {
        private readonly Observable<Weight> _weight;

        public Weighter(Observable<Weight> weight)
        {
            _weight = weight;
        }

        public void UpdateWeight()
        {
            _weight.Value = new Weight(GetWeight());
        }

        protected abstract float GetWeight();
    }
}