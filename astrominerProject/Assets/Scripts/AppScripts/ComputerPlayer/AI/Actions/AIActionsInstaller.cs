using System.Collections;
using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class AIActionsInstaller : MonoInstaller
    {
        public override void InstallBindings(Binder binder)
        {
            binder.Bind<Factory<List<AIAction>, Player>>()
                .ToNew<AIActionsFactory>();
            
            binder.Bind<Factory<FlyToRandomAsteroidAction, Player>>()
                .ToNew<BasicFactory<FlyToRandomAsteroidAction, Player>>();
        }
    }
}
