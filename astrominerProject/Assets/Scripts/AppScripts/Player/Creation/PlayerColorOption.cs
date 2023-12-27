using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    [Serializable]
    public class PlayerColorOption
    {
        [field: SerializeField]
        public Color Color { get; private set; }
        
        [field: SerializeField]
        public string DefaultPlayerName { get; private set; }
    }
}
