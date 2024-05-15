using System;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class BasesPlacementContext
    {
        public Observable<Player> CurrentPlayer { get; } = new Observable<Player>() { Value = null };
    }
}
