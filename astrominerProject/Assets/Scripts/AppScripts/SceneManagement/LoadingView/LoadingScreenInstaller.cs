using System;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class LoadingScreenInstaller : MonoInstaller, Injectable
    {
        [SerializeField] 
        private Displayer _displayer;
        
        private Process.Process _process;

        private void Reset()
        {
            _displayer = GetComponent<Displayer>();
        }

        public void Inject(Resolver resolver)
        {
            _process = resolver.Resolve<Process.Process>();
        }
        
        public override void InstallBindings(Binder binder)
        {
            binder.BindInstance(_process).
                WithoutInjection();
            binder.BindInstance(_displayer).
                WithoutInjection();
        }
    }
}