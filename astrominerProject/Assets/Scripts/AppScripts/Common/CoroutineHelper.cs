using UnityEngine;

namespace SBaier.Astrominer
{
    public class CoroutineHelper : MonoBehaviour
    {
        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}
