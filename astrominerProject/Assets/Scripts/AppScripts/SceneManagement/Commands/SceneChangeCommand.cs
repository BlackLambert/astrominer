using System;
using UnityEngine;

namespace SBaier.Astrominer
{
    [Serializable]
    public abstract class SceneChangeCommand : ScriptableObject
    {
        [field: SerializeField]
        public string SceneName { get; private set; }

        public abstract AsyncOperation Execute();
    }
}
