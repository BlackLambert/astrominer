using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PlayerValue
    {
        public Observable<float> TotalValue { get; } = 0;
        public ObservableList<float> ValueHistory { get; private set; } = new ObservableList<float>();
    }
}
