using System;
using UnityEngine;

namespace SBaier.Astrominer
{
    [RequireComponent(typeof(RectTransform))]
    public class PlayerValueGraphs : MonoBehaviour
    {
        [SerializeField] 
        private PlayerValueGraphCreator _graphCreator;
        
        public RectTransform RectTransform { get; private set; }

        private void Awake()
        {
            RectTransform = transform as RectTransform;
        }

        public void Clean()
        {
            _graphCreator.Clean();
        }
    }
}
