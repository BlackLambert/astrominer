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
            throw new System.NotImplementedException();
        }
    }
}