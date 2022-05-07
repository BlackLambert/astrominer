using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SBaier.Astrominer
{
    [CreateAssetMenu(fileName = "SceneLoadCommand", menuName = "ScriptableObjects/SceneManagement/SceneLoadCommand")]
    public class SceneLoadCommand : SceneManagementCommand
    {
        [field: SerializeField]
        public LoadSceneMode Mode { get; private set; } = LoadSceneMode.Additive;
    }
}
