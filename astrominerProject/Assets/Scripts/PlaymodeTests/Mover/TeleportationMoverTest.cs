using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Astrominer.Test
{
    public class TeleportationMoverTest : MoverTest
    {
        private const string _moverPrefabPath = "Mover/DummyTeleportationMover";
        private const string _uninitializedMoverPrefabPath = "Mover/UninitializedTeleportationMover";

        protected override Mover instantiateMover()
		{
            TeleportationMover prefab = Resources.Load<TeleportationMover>(_moverPrefabPath);
            return GameObject.Instantiate(prefab);
		}

        protected override Mover instantiateUninitializedMover()
        {
            TeleportationMover prefab = Resources.Load<TeleportationMover>(_uninitializedMoverPrefabPath);
            return GameObject.Instantiate(prefab);
        }
    }
}


