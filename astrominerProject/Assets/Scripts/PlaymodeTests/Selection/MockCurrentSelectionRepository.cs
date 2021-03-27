using Moq;
using UnityEngine;

namespace Astrominer.Test
{
    public class MockCurrentSelectionRepository : MonoBehaviour, CurrentSelectionRepository
    {
        public Mock<CurrentSelectionRepository> Mock { get; set; }
        public Selectable CurrentSelection => Mock.Object.CurrentSelection;

        public bool IsEmpty => Mock.Object.IsEmpty;

        public void Deselect()
        {
            Mock.Object.Deselect();
        }

        public bool IsSelected(Selectable selectable)
        {
            return Mock.Object.IsSelected(selectable);
        }

        public void Select(Selectable selectable)
        {
            Mock.Object.Select(selectable);
        }
    }
}