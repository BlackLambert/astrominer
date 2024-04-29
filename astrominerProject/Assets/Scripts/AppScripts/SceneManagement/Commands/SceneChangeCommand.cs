using System;
using UnityEngine;

namespace SBaier.Astrominer
{
    [Serializable]
    public class SceneChangeCommand : ScriptableObject
    {
        [field: SerializeField]
        public string SceneName { get; private set; }
    }
}
