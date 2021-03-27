using System.Collections;
using UnityEngine;
using NUnit.Framework;
using System;
using UnityEngine.EventSystems;
using Moq;
using Zenject;
using UnityEngine.TestTools;

namespace Astrominer.Test
{
    [TestFixture]
    public class OnSelectGameObjectActivatorTest : ZenjectIntegrationTestFixture
    {
        private OnSelectGameObjectActivator _activator;
        private Mock<ObservableSelectable> _selectableMock;
        private GameObject _controlledObject;
        private string _onselectObjectActivtorPrefabPath = "OnSelectObjectActivator/TestOnSelectObjectActivator";
        private int _listenerCount = 0;

        [TearDown]
        public void Dispose()
        {
            if (_activator)
                GameObject.DestroyImmediate(_activator.gameObject);
        }

        [UnityTest]
        public IEnumerator ControlledObjectActivatedAfterSelection()
        {
            GivenADefaultSetup();
            yield return 0;
            WhenSelectableRaisesOnSelectionEvent();
            ThenControlledObjectIsActivated();
        }

        [UnityTest]
        public IEnumerator OnSelectionHasNoListenersAfterTheActivatorIsDestroyed()
		{
            GivenADefaultSetup();
            GivenOnSelectionSetup();
            yield return 0;
            WhenActivatorIsDestroyed();
            yield return 0;
            ThenOnSelectionHasNoListeners();
        }


		private void GivenADefaultSetup()
		{
            PreInstall();
            BindOnSelectObjectActivator();
            BindMockObservableSelectable();
            PostInstall();

            _activator = Container.Resolve<OnSelectGameObjectActivator>();
            _controlledObject = _activator.transform.GetChild(0).gameObject;
        }

        private void GivenOnSelectionSetup()
        {
            _selectableMock.SetupAdd(selectable => selectable.OnSelection += It.IsAny<Action>()).Callback(() => _listenerCount++);
            _selectableMock.SetupRemove(selectable => selectable.OnSelection -= It.IsAny<Action>()).Callback(() => _listenerCount--);
        }

        private void BindOnSelectObjectActivator()
        {
            Container.Bind<OnSelectGameObjectActivator>().FromComponentInNewPrefabResource(_onselectObjectActivtorPrefabPath).AsSingle();
        }

        private void BindMockObservableSelectable()
        {
            _selectableMock = new Mock<ObservableSelectable>();
            Container.Bind<ObservableSelectable>().FromInstance(_selectableMock.Object).AsSingle();
        }

        private void WhenSelectableRaisesOnSelectionEvent()
        {
            _selectableMock.Raise(s => s.OnSelection += null);
        }

        private void WhenActivatorIsDestroyed()
        {
            GameObject.Destroy(_activator.gameObject);
        }

        private void ThenControlledObjectIsActivated()
        {
            Assert.True(_controlledObject.activeSelf);
        }
        private void ThenOnSelectionHasNoListeners()
        {
            Assert.AreEqual(0, _listenerCount);
        }
    }
}


