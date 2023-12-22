using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class MonoPoolInstaller<TItem> : MonoInstaller where TItem : MonoBehaviour
    {
        [SerializeField]
        private TItem _prefab;
        
        public override void InstallBindings(Binder binder)
        {
            binder.Bind<Factory<TItem>>().
                ToNew<PrefabFactory<TItem>>().
                WithArgument<TItem>(_prefab);
            
            binder.Bind<Pool<TItem>>().
                ToNew<MonoPool<TItem>>().
                WithArgument<TItem>(_prefab).
                AsSingle();
        }
    }
    
    public class MonoPoolInstaller<TItem, TArgument> : MonoInstaller where TItem : MonoBehaviour
    {
        [SerializeField]
        private TItem _prefab;
        
        public override void InstallBindings(Binder binder)
        {
            binder.Bind<Factory<TItem, TArgument>>().
                ToNew<PrefabFactory<TItem, TArgument>>().
                WithArgument<TItem>(_prefab);
            
            binder.Bind<Pool<TItem, TArgument>>().
                ToNew<MonoPool<TItem, TArgument>>().
                WithArgument<TItem>(_prefab).
                AsSingle();
        }
    }
}
