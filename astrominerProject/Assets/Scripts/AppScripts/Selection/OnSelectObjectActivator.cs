using System;
using UnityEngine;

namespace Astrominer.Test
{
    public class OnSelectObjectActivator: MonoBehaviour
    {
        [SerializeField]
        private GameObject _controlledObject;
        [SerializeField]
        private ObservableSelectable _selectable;

        protected virtual void Start()
        {
            _selectable.OnSelection += onSelection;
        }

        protected virtual void OnDestroy()
        {
            _selectable.OnSelection -= onSelection;
        }

        private void onSelection()
        {
            _controlledObject.SetActive(true);
        }
    }
}