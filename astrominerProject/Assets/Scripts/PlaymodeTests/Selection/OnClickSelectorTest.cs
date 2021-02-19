using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using System;
using UnityEngine.EventSystems;

namespace Astrominer.Test
{

    public class OnClickSelectorTest
    {
        private OnClickSelector _selector;
        private DummyCurrentSelectionRepository _dummyRepository;
        private string _onClickSelectorPath = "Selector/TestOnClickSelector";

        [TearDown]
        public void Dispose()
        {
            if (_selector)
                GameObject.Destroy(_selector.gameObject);
            if (_dummyRepository)
                GameObject.Destroy(_dummyRepository.gameObject);
        }

        [Test]
        public void OnPointerClick_SelectCalledOnRepository()
        {
            GivenNewSelector();
            bool called = WhenOnPointerClickIsCalledWithObservedRepository();
            ThenSelectIsCalledOnRepository(called);
        }

        private bool WhenOnPointerClickIsCalledWithObservedRepository()
        {
            bool called = false;
            Action onSelectCalled = () => called = true;
            _dummyRepository.OnSelect += onSelectCalled;
            WhenOnPointerClickIsCalled();
            _dummyRepository.OnSelect -= onSelectCalled;
            return called;
        }

        private void ThenSelectIsCalledOnRepository(bool called)
        {
            Assert.True(called);
        }

        private void GivenNewSelector()
        {
            _dummyRepository = CreateDummyRepository();
            OnClickSelector prefab = Resources.Load<OnClickSelector>(_onClickSelectorPath);
            _selector = GameObject.Instantiate(prefab);
        }

        private DummyCurrentSelectionRepository CreateDummyRepository()
        {
            GameObject obj = new GameObject();
            return obj.AddComponent<DummyCurrentSelectionRepository>();
        }

        private void WhenOnPointerClickIsCalled()
        {
            (_selector as IPointerClickHandler).OnPointerClick(null);
        }
    }
}