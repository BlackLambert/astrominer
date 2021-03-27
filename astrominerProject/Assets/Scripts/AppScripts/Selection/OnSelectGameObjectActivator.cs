using System;
using UnityEngine;
using Zenject;

namespace Astrominer.Test
{
    public class OnSelectGameObjectActivator: MonoBehaviour
    {
        [SerializeField]
        private GameObject _controlledObject;
        private ObservableSelectable _selectable;

        [Inject]
        public void Construct(ObservableSelectable selectable)
		{
            _selectable = selectable;

        }

        protected virtual void Start()
        {
            _selectable.OnSelection += onSelection;
        }

        private void onSelection()
        {
            _controlledObject.SetActive(true);
        }

		private void OnDestroy()
		{
            _selectable.OnSelection -= onSelection;
		}
	}
}