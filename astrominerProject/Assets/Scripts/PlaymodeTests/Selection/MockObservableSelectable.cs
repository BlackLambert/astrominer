using System;
using Moq;

namespace Astrominer.Test
{
    public class MockObservableSelectable : ObservableSelectable
    {
        public Mock<ObservableSelectable> Mock { get; set; }

        public bool IsSelected => Mock.Object.IsSelected;

        public event Action OnSelection
        {
            add => Mock.Object.OnSelection += value;
            remove => Mock.Object.OnSelection -= value;
        }
        public event Action OnDeselection
        {
            add => Mock.Object.OnDeselection += value;
            remove => Mock.Object.OnDeselection -= value;
        }

        public void Select()
        {
            Mock.Object.Select();
        }

        public void Deselect()
        {
            Mock.Object.Deselect();
        }
    }
}