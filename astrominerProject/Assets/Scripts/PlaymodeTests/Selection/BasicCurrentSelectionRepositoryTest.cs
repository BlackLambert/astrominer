using System;
using Moq;
using NUnit.Framework;
using UnityEngine;
using Zenject;

namespace Astrominer.Test
{
	[TestFixture]
    public class BasicCurrentSelectionRepositoryTest : ZenjectUnitTestFixture
    {
		private CurrentSelectionRepository _repository;
		private Mock<Selectable> _selectableMock;
		private Selectable _selectable;
		private Selectable _secondSelectable;


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
			GivenASelectSetup();
			WhenSelectCalledWithSelectable();
			ThenTheSelectOfSelectableIsCalled();
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
			GivenADeselectSetup();
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

		[Test]
		public void IsSelected_ReturnsTrueOnCurrentSelectionIsTheGivenArgument()
		{
			GivenARepositoryWithSelection();
			bool selected = WhenIsSelectedCalledWithCurrentSelection();
			ThenIsSelectedReturnsTrue(selected);
		}

		[Test]
		public void IsSelected_ReturnsFalseOnCurrentSelectionIsNotTheGivenArgument()
		{
			GivenARepositoryWithSelection();
			bool selected = WhenIsSelectedCalledWithOtherThanCurrentSelection();
			ThenIsSelectedReturnsFalse(selected);
		}

		[Test]
		public void IsSelected_ThrowsExceptionOnArgumentNull()
		{
			GivenANewRepository();
			TestDelegate test = () => WhenIsSelectedCalledWithNullArgument();
			ThenAnArgumentNullExceptionIsThrown(test);
		}

		private void GivenANewRepository()
		{
			Container.Bind<CurrentSelectionRepository>().To<BasicCurrentSelectionRepository>().AsSingle();
			_selectableMock = new Mock<Selectable>();
			Container.Bind<Selectable>().To<Selectable>().FromInstance(_selectableMock.Object).AsSingle();

			_repository = Container.Resolve<CurrentSelectionRepository>();
			_selectable = Container.Resolve<Selectable>();
		}

		private void GivenARepositoryWithSelection()
		{
			GivenANewRepository();
			_repository.Select(_selectable);
			Mock<Selectable> secondSelectableMock = new Mock<Selectable>();
			_secondSelectable = secondSelectableMock.Object;
		}

		private void GivenASelectSetup()
		{
			_selectableMock.Setup(selectable => selectable.Select());
		}

		private void GivenADeselectSetup()
		{
			_selectableMock.Setup(selectable => selectable.Deselect());
		}

		private void WhenIsSelectedCalledWithNullArgument()
		{
			_repository.IsSelected(null);
		}

		private bool WhenIsSelectedCalledWithOtherThanCurrentSelection()
		{
			return _repository.IsSelected(_secondSelectable);
		}

		private bool WhenIsSelectedCalledWithCurrentSelection()
		{
			return _repository.IsSelected(_selectable);
		}

		private void WhenSelectCalledWithSelectable()
		{
			_repository.Select(_selectable);
		}

		private void WhenSelectedTwice()
		{
			_repository.Select(_selectable);
			_repository.Select(_selectable);
		}

		private void WhenSelectCalledWithSecondSelectable()
		{
			_repository.Select(_secondSelectable);
		}

		private void WhenDeselectIsCalled()
		{
			_repository.Deselect();
		}

		private void WhenSelectArgumentIsNull()
		{
			_repository.Select(null);
		}

		private void ThenIsSelectedReturnsFalse(bool selected)
		{
			Assert.False(selected);
		}

		private void ThenIsSelectedReturnsTrue(bool selected)
		{
			Assert.True(selected);
		}

		private void ThenSelectionIsTheGivenSelectable()
		{
			Assert.AreEqual(_selectable, _repository.CurrentSelection);
		}

		private void ThenTheSelectionIsSelected()
		{
			Assert.IsTrue(_selectable.IsSelected);
		}

		private void ThenAnAlreadySelectedExceptionIsThrown(TestDelegate testDelegate)
		{
			Assert.Throws<BasicCurrentSelectionRepository.AlreadySelectedException>(testDelegate);
		}

		private void ThenTheSelectOfSelectableIsCalled()
		{
			_selectableMock.Verify(selectable => selectable.Select(), Times.Once);
		}

		private void ThenTheFormerSelectionWillBeDeselected()
		{
			_selectableMock.Verify(selectable => selectable.Deselect(), Times.Once);
		}

		private void ThenAnArgumentNullExceptionIsThrown(TestDelegate testDelegate)
		{
			Assert.Throws<ArgumentNullException>(testDelegate);
		}

		private void ThenACurrentSelectionIsNullExceptionIsThrown(TestDelegate testDelegate)
		{
			Assert.Throws<BasicCurrentSelectionRepository.CurrentSelectionIsNullException>(testDelegate);
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