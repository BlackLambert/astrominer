using System.Linq;
using SBaier.AI;

namespace SBaier.Astrominer
{
    public class SellMachineWeighter : Weighter
    {
        private readonly SellMachineAISettings _settings;
        private readonly AIBrain _brain;

        public SellMachineWeighter(
            AIBrain brain,
            SellMachineAISettings settings)
        {
            _settings = settings;
            _brain = brain;
        }
        
        public float GetWeight()
        {
            float weight = _settings.BaseWeight;
            
            // Any occupation target?
            Asteroid occupationTarget = _brain.OccupationTargets.FirstOrDefault();
            weight += occupationTarget == null ? _settings.NoOccupationTargetWeight : 0;

            // Value of occupation target
            weight += _settings.BestOccupationTargetCurve.Evaluate(_brain.GetOccupationValueOf(occupationTarget))
                * _settings.BestOccupationTargetFactor;
            
            // exploiters amount
            int exploitersAmount = _brain.ExploitersInInventoryAmount;
            weight += _settings.ExploiterAmountValueCurve.Evaluate(exploitersAmount) * 
                      _settings.ExploiterAmountFactor;

            return weight;
        }
    }
}