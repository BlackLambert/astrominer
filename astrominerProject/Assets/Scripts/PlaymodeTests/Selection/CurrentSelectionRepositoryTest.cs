using System;
using NUnit.Framework;
using UnityEngine;

namespace Astrominer.Test
{
    public class CurrentSelectionRepositoryTest
    {
		private CurrentSelectionRepository _repository;
		private Selectable _selectable;
		private Selectable _secondSelectable;

		[TearDown]
		public void Dispose()
		{
			if(_selectable != null)
				GameObject.Destroy(_selectable.gameObject);
			if (_secondSelectable != null)
				GameObject.Destroy(_secondSelectable.gameObject);
		}

		[Test]
        public void Select_CurrentSelectionIsGivenSelectable()
		{
            GivenANewRepository();
            WhenSelectCalledWithSelectable();
            ThenSelectionIsTheGivenSelectable();
		}

		[Test]
		public void Select_CurrentSelectionIsSelected()
		{
			GivenANewRepository();
			WhenSelectCalledWithSelectable();
			ThenTheSelectionIsSelected();
		}

		[Test]
		public void Select_ReselectionThrowsException()
		{
			GivenANewRepository();
			TestDelegate test = () => WhenSelectedTwice();
			ThenAnAlreadySelectedExceptionIsThrown(test);
		}

		[Test]
		public void Select_FormerSelectionIsDeselected()
		{
			GivenARepositoryWithSelection();
			WhenSelectCalledWithSecondSelectable();
			ThenTheFormerSelectionWillBeDeselected();
		}

		[Test]
		public void Select_NullArgumentThrowsException()
		{
			GivenANewRepository();
			TestDelegate test = () => WhenSelectArgumentIsNull();
			ThenAnArgumentNullExceptionIsThrown(test);
		}

		[Test]
		public void Deselect_ThrowsExceptionOnNoCurrentSelection()
		{
			GivenANewRepository();
			TestDelegate test = () => WhenDeselectIsCalled();
			ThenACurrentSelectionIsNullExceptionIsThrown(test);
		}

		[Test]
		public void Deselect_FormerSelectionGetsDeselected()
		{
			GivenARepositoryWithSelection();
			WhenDeselectIsCalled();
			ThenTheFormerSelectedIsDeselected();
		}

		[Test]
		public void Deselect_CurrentSelectionBecomesNull()
		{
			GivenARepositoryWithSelection();
			WhenDeselectIsCalled();
			ThenTheCurrentSelectionBecomesNull();
		}

		[Test]
		public void IsEmpty_ReturnsTrueOnCurrentSelectionNull()
		{
			GivenANewRepository();
			ThenIsEmptyReturnsTrue();
		}

		[Test]
		public void IsEmpty_ReturnsFalseOnCurrentSelectionNotNull()
		{
			GivenARepositoryWithSelection();
			ThenIsEmptyReturnsFalse();
		}

		private void GivenANewRepository()
		{
			_repository = new CurrentSelectionRepository();
			_selectable = createDummySelectable();
		}
		private Selectable createDummySelectable()
		{
			GameObject selectableObject = new GameObject();
			return selectableObject.AddComponent<DummySelectable>();
		}

		private void WhenSelectCalledWithSelectable()
		{
			_repository.Select(_selectable);
		}

		private void ThenSelectionIsTheGivenSelectable()
		{
			Assert.AreEqual(_selectable, _repository.CurrentSelection);
		}

		private void ThenTheSelectionIsSelected()
		{
			Assert.IsTrue(_selectable.IsSelected);
		}


		private void WhenSelectedTwice()
		{
			_repository.Select(_selectable);
			_repository.Select(_selectable);
		}

		private void ThenAnAlreadySelectedExceptionIsThrown(TestDelegate testDelegate)
		{
			Assert.Throws<CurrentSelectionRepository.AlreadySelectedException>(testDelegate);
		}

		private void GivenARepositoryWithSelection()
		{
			GivenANewRepository();
			_repository.Select(_selectable);
			_secondSelectable = createDummySelectable();
		}

		private void WhenSelectCalledWithSecondSelectable()
		{
			_repository.Select(_secondSelectable);
		}

		private void ThenTheFormerSelectionWillBeDeselected()
		{
			Assert.False(_selectable.IsSelected);
		}

		private void WhenSelectArgumentIsNull()
		{
			_repository.Select(null);
		}

		private void ThenAnArgumentNullExceptionIsThrown(TestDelegate testDelegate)
		{
			Assert.Throws<ArgumentNullException>(testDelegate);
		}

		private void WhenDeselectIsCalled()
		{
			_repository.Deselect();
		}

		private void ThenACurrentSelectionIsNullExceptionIsThrown(TestDelegate testDelegate)
		{
			Assert.Throws<CurrentSelectionRepository.CurrentSelectionIsNullException>(testDelegate);
		}

		private void ThenTheFormerSelectedIsDeselected()
		{
			Assert.IsFalse(_selectable.IsSelected);
		}

		private void ThenTheCurrentSelectionBecomesNull()
		{
			Assert.Null(_repository.CurrentSelection);
		}

		private void ThenIsEmptyReturnsTrue()
		{
			Assert.IsTrue(_repository.IsEmpty);
		}

		private void ThenIsEmptyReturnsFalse()
		{
			Assert.IsFalse(_repository.IsEmpty);
		}
	}
}