using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Astrominer.Test
{
    public class DummySelectable : MonoBehaviour, Selectable
    {
        public bool IsSelected => throw new System.NotImplementedException();

        public void Deselect()
        {
            throw new System.NotImplementedException();
        }

        public void Select()
        {
            throw new System.NotImplementedException();
        }
    }
}