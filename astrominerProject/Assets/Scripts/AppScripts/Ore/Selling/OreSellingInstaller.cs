using System;
using SBaier.DI;

namespace SBaier.Astrominer
{
    public class OreSellingInstaller : MonoInstaller
    {
        public override void InstallBindings(Binder binder)
        {
            binder.BindToNewSelf<OreBank>();
            binder.BindToNewSelf<OreValue>();

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
