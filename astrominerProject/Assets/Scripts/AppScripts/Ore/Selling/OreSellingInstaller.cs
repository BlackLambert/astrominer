using System;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class OreSellingInstaller : MonoInstaller
    {
        [SerializeField] 
        private int _formerOreValueBufferSize = 100;
        
        public override void InstallBindings(Binder binder)
        {
            binder.BindToNewSelf<OreBank>().AsSingle();
            binder.Bind<OreValue>().ToInstance(new OreValue(_formerOreValueBufferSize));

            foreach (OreType oreType in Enum.GetValues(typeof(OreType)))
            {
                if (oreType == OreType.None)
                {
                    continue;
                }
                
                string oreTypeName = oreType.ToString();
                binder.BindComponent<OreValueCalculator>(oreTypeName)
                    .FromNewComponentOnNewGameObject($"OreCalculator{oreTypeName}", transform)
                    .WithArgument(oreType)
                    .AsNonResolvable();
            }
        }
    }
}
