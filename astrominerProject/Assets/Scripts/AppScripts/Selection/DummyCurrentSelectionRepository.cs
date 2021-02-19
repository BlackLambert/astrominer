using System;

namespace Astrominer
{
    public class DummyCurrentSelectionRepository : CurrentSelectionRepository
    {
        public override Selectable CurrentSelection => throw new System.NotImplementedException();
        public event Action OnSelect;

        public override void Deselect()
        {
            throw new System.NotImplementedException();
        }

        public override void Select(Selectable selectable)
        {
            OnSelect?.Invoke();
        }
    }
}