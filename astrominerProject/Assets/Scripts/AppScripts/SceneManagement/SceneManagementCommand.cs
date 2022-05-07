using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    [Serializable]
    public class SceneManagementCommand : ScriptableObject
    {
        [field: SerializeField]
        public string SceneName { get; private set; }
    }
}
