using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using System;
using UnityEngine.EventSystems;
using Moq;
using Zenject;

namespace Astrominer.Test
{
    [TestFixture]
    public class OnClickSelectorTest : ZenjectIntegrationTestFixture
    {
        private const string _onClickSelectorPath = "Selector/TestOnClickSelector";
        private OnClickSelector _selector;
        private Mock<CurrentSelectionRepository> _repositoryMock;
        private Selectable _selectable;

        [TearDown]
        public void Dispose()
        {
            if (_selector)
                GameObject.DestroyImmediate(_selector.gameObject);
        }

        [Test]
        public void OnPointerClick_SelectCalledOnRepository()
        {
            GivenADefaultSetup();
            WhenOnPointerClickIsCalled();
            ThenSelectIsCalledOnRepository();
        }

        [Test]
        public void OnPointerClick_RepositorySelectReceivesPredictedSelectable()
        {
            GivenADefaultSetup();
            WhenOnPointerClickIsCalled();
            ThenArgumentIsPredictedSelectable();
        }

        [Test]
        public void OnPointerClick_RepositorySelectNotCalledIfAlreadySelected()
        {
            GivenASetupWithIsSelectedTrue();
            WhenOnPointerClickIsCalled();
            ThenSelectIsNotCalledOnRepository();
        }

        private void GivenADefaultSetup()
		{
            Install();
            _selector = Container.Resolve<OnClickSelector>();
            _selectable = Container.Resolve<Selectable>();
            GivenSelectIsSetUpOnMock();
        }

		private void GivenASetupWithIsSelectedTrue()
		{
			Install();
			_selector = Container.Resolve<OnClickSelector>();
            _selectable = Container.Resolve<Selectable>();
            GivenSelectIsSetUpOnMock();
			GivenIsSelectedIsSetTrueOnMock();
		}

		private void Install()
		{
			PreInstall();
			BindMockRepository();
			BindSelectable();
			BindSelector();
			PostInstall();
		}

		private void BindMockRepository()
        {
            _repositoryMock = new Mock<CurrentSelectionRepository>();
            Container.Bind<CurrentSelectionRepository>().FromInstance(_repositoryMock.Object).AsSingle();
        }

        private void BindSelectable()
        {
            Mock<Selectable> selectableMock = new Mock<Selectable>();
            Container.Bind<Selectable>().FromInstance(selectableMock.Object).AsSingle();
        }

        private void BindSelector()
        {
            Container.Bind<OnClickSelector>().FromComponentInNewPrefabResource(_onClickSelectorPath).AsSingle();
        }

        private void GivenSelectIsSetUpOnMock()
        {
            _repositoryMock.Setup(r => r.Select(_selectable));
        }

        private void GivenIsSelectedIsSetTrueOnMock()
        {
            _repositoryMock.Setup(r => r.IsSelected(_selectable)).Returns(true);
        }

        private void WhenOnPointerClickIsCalled()
        {
            (_selector as IPointerClickHandler).OnPointerClick(null);
        }

        private void ThenArgumentIsPredictedSelectable()
        {
            _repositoryMock.Verify(r => r.Select(_selectable), Times.Once());
        }

        private void ThenSelectIsNotCalledOnRepository()
        {
            _repositoryMock.Verify(r => r.Select(_selectable), Times.Never());
        }

        private void ThenSelectIsCalledOnRepository()
        {
            _repositoryMock.Verify(r => r.Select(_selectable), Times.Once());
        }


	}
}