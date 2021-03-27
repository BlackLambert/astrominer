using System.Collections;
using UnityEngine;
using NUnit.Framework;
using System;
using UnityEngine.EventSystems;
using Moq;

namespace Astrominer.Test
{
    public class OnSelectObjectActivatorTest
    {
        private OnSelectObjectActivator _activator;
        private MockObservableSelectable _selectable;
        private GameObject _controlledObject;
        private string _onselectObjectActivtorPrefabPath = "OnSelectObjectActivator/TestOnSelectObjectActivator";

        [TearDown]
        public void Dispose()
        {
            if (_activator)
                GameObject.DestroyImmediate(_activator.gameObject);
        }

        [Test]
        public void ControlledObjectActivatedAfterSelection()
        {
            GivenNewOnSelectObjectActivator();
            GivenMockObservableSelectable();
            WhenSelectableRaisesOnSelectionEvent();
            ThenControlledObjectIsActivated();
        }

        private void GivenNewOnSelectObjectActivator()
        {
            OnSelectObjectActivator prefab = Resources.Load<OnSelectObjectActivator>(_onselectObjectActivtorPrefabPath);
            _activator = GameObject.Instantiate(prefab);
            _controlledObject = _activator.transform.GetChild(0).gameObject;
        }

        private void GivenMockObservableSelectable()
        {
            _selectable = _activator.GetComponent<MockObservableSelectable>();
            _selectable.Mock = new Mock<ObservableSelectable>();
        }

        private void WhenSelectableRaisesOnSelectionEvent()
        {
            _selectable.Mock.Raise(s => s.OnSelection += null);
        }

        private void ThenControlledObjectIsActivated()
        {
            Assert.True(_controlledObject.activeSelf);
        }
    }
}


