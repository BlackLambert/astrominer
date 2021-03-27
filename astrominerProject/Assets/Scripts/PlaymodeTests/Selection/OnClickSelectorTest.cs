using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using System;
using UnityEngine.EventSystems;
using Moq;

namespace Astrominer.Test
{

    public class OnClickSelectorTest
    {
        private OnClickSelector _selector;
        private MockCurrentSelectionRepository _mockRepository;
        private string _onClickSelectorPath = "Selector/TestOnClickSelector";

        [TearDown]
        public void Dispose()
        {
            if (_selector)
                GameObject.DestroyImmediate(_selector.gameObject);
            if (_mockRepository)
                GameObject.DestroyImmediate(_mockRepository.gameObject);
        }

        [Test]
        public void OnPointerClick_SelectCalledOnRepository()
        {
            GivenAMockRepositoryWithSelect();
            GivenNewSelector();
            GivenSelectIsSetUpOnMock();
            WhenOnPointerClickIsCalled();
            ThenSelectIsCalledOnRepository();
        }

        [Test]
        public void OnPointerClick_RepositorySelectReceivesPredictedSelectable()
        {
            GivenAMockRepositoryWithSelect();
            GivenNewSelector();
            GivenSelectIsSetUpOnMock();
            WhenOnPointerClickIsCalled();
            ThenArgumentIsPredictedSelectable();
        }

        [Test]
        public void OnPointerClick_RepositorySelectNotCalledIfAlreadySelected()
        {
            GivenAMockRepositoryWithSelect();
            GivenNewSelector();
            GivenSelectIsSetUpOnMock();
            GivenIsSelectedIsSetTrueOnMock();
            WhenOnPointerClickIsCalled();
            ThenSelectIsNotCalledOnRepository();
        }

        private void GivenSelectIsSetUpOnMock()
        {
            _mockRepository.Mock.Setup(r => r.Select(_selector.GetComponent<Selectable>()));
        }


        private void GivenAMockRepositoryWithSelect()
        {
            _mockRepository = CreateMockRepository();
        }

        private void GivenIsSelectedIsSetTrueOnMock()
        {
            _mockRepository.Mock.Setup(r => r.IsSelected(_selector.GetComponent<Selectable>())).Returns(true);
        }

        private void GivenNewSelector()
        {
            OnClickSelector prefab = Resources.Load<OnClickSelector>(_onClickSelectorPath);
            _selector = GameObject.Instantiate(prefab);
        }

        private MockCurrentSelectionRepository CreateMockRepository()
        {
            GameObject obj = new GameObject();
            MockCurrentSelectionRepository mockRepository = obj.AddComponent<MockCurrentSelectionRepository>();
            mockRepository.Mock = new Mock<CurrentSelectionRepository>();
            return mockRepository;
        }

        private void WhenOnPointerClickIsCalled()
        {
            (_selector as IPointerClickHandler).OnPointerClick(null);
        }

        private void ThenArgumentIsPredictedSelectable()
        {
            _mockRepository.Mock.Verify(r => r.Select(_selector.GetComponent<Selectable>()), Times.Once());
        }

        private void ThenSelectIsNotCalledOnRepository()
        {
            _mockRepository.Mock.Verify(r => r.Select(_selector.GetComponent<Selectable>()), Times.Never());
        }

        private void ThenSelectIsCalledOnRepository()
        {
            _mockRepository.Mock.Verify(r => r.Select(_selector.GetComponent<Selectable>()), Times.Once());
        }


	}
}