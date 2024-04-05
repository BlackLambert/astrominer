using UnityEngine;

namespace SBaier.Astrominer
{
    public class StandaloneAppQuitter : AppQuitter
    {
        public override void Quit()
        {
            Application.Quit();
        }
    }
}
