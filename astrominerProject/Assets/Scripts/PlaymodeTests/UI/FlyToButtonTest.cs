using Zenject;
using System.Collections;
using UnityEngine.TestTools;
using System;
using UnityEngine.UI;
using NUnit.Framework;
using Moq;

namespace Astrominer.Test
{
    public class FlyToButtonTest : ZenjectIntegrationTestFixture
    {
        private Button _button;
        private Mock<CurrentSelectionRepository> _currentSelectionRepositoryMock;
        private const string _flyToButtonPrefabPath = "FlyToButton/TestFlyToButton";
        private FlyToButton _flyToButton;

        [UnityTest]
        public IEnumerator Start_ButtonDeactivatedWhenNoSelection()
        {
            // Setup initial state by creating game objects from scratch, loading prefabs/scenes, etc
            GivenDefaultSetup();
            GivenNoSelection();
            yield return 0;
            ThenButtonIsDeactivated();
        }

        private void GivenNoSelection()
        {
            _currentSelectionRepositoryMock.SetupGet(repository => repository.CurrentSelection).Returns((Selectable)null);
            _currentSelectionRepositoryMock.SetupGet(repository => repository.IsEmpty).Returns(true);
        }

        private void ThenButtonIsDeactivated()
        {
            Assert.False(_button.interactable);
        }

        private void GivenDefaultSetup()
        {
            _currentSelectionRepositoryMock = new Mock<CurrentSelectionRepository>();
            PreInstall();

            Container.Bind<FlyToButton>().FromComponentInNewPrefabResource(_flyToButtonPrefabPath).AsSingle();
            Container.Bind<CurrentSelectionRepository>().FromInstance(_currentSelectionRepositoryMock.Object).AsSingle();

            PostInstall();
            _flyToButton = Container.Resolve<FlyToButton>();
            _button = _flyToButton.GetComponentInChildren<Button>();
        }
    }
}

