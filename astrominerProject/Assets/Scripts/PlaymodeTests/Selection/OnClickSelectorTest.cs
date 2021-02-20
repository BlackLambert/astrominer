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
                GameObject.DestroyImmediate(_selector.gameObject);
            if (_dummyRepository)
                GameObject.DestroyImmediate(_dummyRepository.gameObject);
        }

        [Test]
        public void OnPointerClick_SelectCalledOnRepository()
        {
            GivenADummyRepository();
            GivenNewSelector();
            using (var observer = new RepositorySelectObserver(_dummyRepository))
            {
                WhenOnPointerClickIsCalled();
                ThenSelectIsCalledOnRepository(observer.Called);
            }
        }

        [Test]
        public void OnPointerClick_RepositorySelectReceivesPredictedSelectable()
		{
            GivenADummyRepository();
            GivenNewSelector();
            using (var observer = new RepositorySelectObserver(_dummyRepository))
            {
                WhenOnPointerClickIsCalled();
                ThenArgumentIsPredictedSelectable(observer.GivenArgument);
            }
        }

		[Test]
        public void OnPointerClick_RepositorySelectNotCalledIfAlreadySelected()
		{
            GivenADummyRepository();
            GivenNewSelector();
            using (var observer = new RepositorySelectObserver(_dummyRepository))
            {
                WhenOnPointerClickIsCalled();
                ThenSelectIsNotCalledOnRepository(observer.Called);
            }
        }

		private void ThenSelectIsNotCalledOnRepository(bool called)
		{
            Assert.False(called);
		}

		private void GivenADummyRepository()
        {
            _dummyRepository = CreateSelectDummyRepository();
        }

		private void ThenArgumentIsPredictedSelectable(Selectable givenArgument)
		{
            Assert.AreEqual(_selector.GetComponent<Selectable>(), givenArgument);
		}

		private void ThenSelectIsCalledOnRepository(bool called)
        {
            Assert.True(called);
        }

        private void GivenNewSelector()
        {
            OnClickSelector prefab = Resources.Load<OnClickSelector>(_onClickSelectorPath);
            _selector = GameObject.Instantiate(prefab);
        }

        private DummyCurrentSelectionRepository CreateSelectDummyRepository()
        {
            GameObject obj = new GameObject();
            return obj.AddComponent<DummyCurrentSelectionRepository>();
        }

        private void WhenOnPointerClickIsCalled()
        {
            (_selector as IPointerClickHandler).OnPointerClick(null);
        }

		private class RepositorySelectObserver : IDisposable
		{
            public bool Called => GivenArgument != null;
            public Selectable GivenArgument { get; private set; } = null;

            private DummyCurrentSelectionRepository _repository;

            public RepositorySelectObserver(DummyCurrentSelectionRepository repository)
			{
                _repository = repository;
                _repository.OnSelect += onSelect;
            }

			private void onSelect(Selectable selection)
			{
                GivenArgument = selection;
            }

			public void Dispose()
			{
                _repository.OnSelect -= onSelect;
            }
		}
	}
}