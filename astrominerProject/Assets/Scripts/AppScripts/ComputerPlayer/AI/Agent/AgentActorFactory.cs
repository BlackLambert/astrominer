using System;
using System.Collections.Generic;
using SBaier.AI;
using SBaier.DI;

namespace SBaier.Astrominer
{
    public class AgentActorFactory : Factory<AgentActor, Ship>, Injectable
    {
        private BuyExploiterActionSettings _buyExploiterSettings;
        private SendProspectorDroneActionSettings _sendProspectorDroneSettings;
        private ExploitMachineSettings _exploitMachineSettings;
        private ExploitMachineVendor _vendor;
        private CosmicObjectInRangeGetter _inRangeGetter;
        private Random _random;
        private Map _map;
        private Bases _bases;
        private DroneBuyer<ProspectorDrone> _droneBuyer;

        public void Inject(Resolver resolver)
        {
            _buyExploiterSettings = resolver.Resolve<BuyExploiterActionSettings>();
            _sendProspectorDroneSettings = resolver.Resolve<SendProspectorDroneActionSettings>();
            _exploitMachineSettings = resolver.Resolve<ExploitMachineSettings>();
            _vendor = resolver.Resolve<ExploitMachineVendor>();
            _inRangeGetter = resolver.Resolve<CosmicObjectInRangeGetter>();
            _random = resolver.Resolve<Random>();
            _map = resolver.Resolve<Map>();
            _bases = resolver.Resolve<Bases>();
            _droneBuyer = resolver.Resolve<DroneBuyer<ProspectorDrone>>();
        }

        public AgentActor Create(Ship ship)
        {
            Observable<bool> allowsFollowupAction = new Observable<bool>() { Value = false };
            List<Weighter> weighters = new List<Weighter>();
            WeightedSelector selector = new WeightedSelector();

            SequenceCreationResult buyExploiterSequence = CreateBuyExploiterSequence(ship, allowsFollowupAction);
            selector.AddChild(buyExploiterSequence.Node);
            weighters.Add(buyExploiterSequence.Weighter);

            selector.AddChild(CreateFlyToRandomAsteroidNode(ship, allowsFollowupAction));

            SequenceCreationResult sendProspectorDrone = CreateSendProspectorDroneSequence(ship, allowsFollowupAction);
            selector.AddChild(sendProspectorDrone.Node);
            weighters.Add(sendProspectorDrone.Weighter);

            return new AgentActor(selector, weighters, allowsFollowupAction);
        }

        private SequenceCreationResult CreateBuyExploiterSequence(Ship ship, Observable<bool> allowsFollowupAction)
        {
            CanPurchaseExploiterCondition canPurchaseExploiterCondition =
                new CanPurchaseExploiterCondition(ship, _exploitMachineSettings);
            HasEmptyInventorySpaceCondition hasEmptyInventorySpaceCondition =
                new HasEmptyInventorySpaceCondition(ship);
            ShipLocationIsPlayerBaseCondition shipLocationIsPlayerBaseCondition =
                new ShipLocationIsPlayerBaseCondition(ship);

            BuyExploiterAction action = new BuyExploiterAction(
                _exploitMachineSettings, _vendor, ship, allowsFollowupAction);

            Sequence buyExploiterSequence = new Sequence();
            buyExploiterSequence.AddChild(canPurchaseExploiterCondition);
            buyExploiterSequence.AddChild(hasEmptyInventorySpaceCondition);
            buyExploiterSequence.AddChild(shipLocationIsPlayerBaseCondition);
            buyExploiterSequence.AddChild(action);

            Observable<Weight> weight = new Observable<Weight>() { Value = new Weight() };
            WeightedNode weightedSequence = new WeightedNode(buyExploiterSequence, weight);

            BuyExploiterWeighter weighter = new BuyExploiterWeighter(
                weight, _buyExploiterSettings, ship);

            return new SequenceCreationResult()
            {
                Weighter = weighter,
                Node = weightedSequence
            };
        }

        private WeightedNode CreateFlyToRandomAsteroidNode(Ship ship, Observable<bool> allowsFollowupAction)
        {
            FlyToRandomCosmicObjectAction action = new FlyToRandomCosmicObjectAction(
                ship, _random.CreateWithNewSeed(), _inRangeGetter, allowsFollowupAction);
            Observable<Weight> weight = new Observable<Weight>() { Value = new Weight(0) };
            return new WeightedNode(action, weight);
        }

        private SequenceCreationResult CreateSendProspectorDroneSequence(Ship ship,
            Observable<bool> allowsFollowupAction)
        {
            CanPurchaseCondition canPurchaseCondition =
                new CanPurchaseCondition(ship.Player, _droneBuyer.CostsPerDrone);
            AnyUnidentifiedAsteroidsCondition anyUnidentifiedAsteroidsCondition =
                new AnyUnidentifiedAsteroidsCondition(ship.Player, _map);

            SendProspectorDroneAction action = new SendProspectorDroneAction(_sendProspectorDroneSettings, 
                _droneBuyer, ship, _map, _bases, allowsFollowupAction);

            Sequence buyExploiterSequence = new Sequence();
            buyExploiterSequence.AddChild(canPurchaseCondition);
            buyExploiterSequence.AddChild(anyUnidentifiedAsteroidsCondition);
            buyExploiterSequence.AddChild(action);

            Observable<Weight> weight = new Observable<Weight>() { Value = new Weight() };
            WeightedNode weightedSequence = new WeightedNode(buyExploiterSequence, weight);

            SendProspectorDroneWeighter weighter =
                new SendProspectorDroneWeighter(weight, _map, ship, _sendProspectorDroneSettings);

            return new SequenceCreationResult()
            {
                Weighter = weighter,
                Node = weightedSequence
            };
        }

        private struct SequenceCreationResult
        {
            public Weighter Weighter;
            public WeightedNode Node;
        }
    }
}