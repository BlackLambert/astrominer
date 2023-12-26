using System;

namespace SBaier.Astrominer
{
    public class Selection
    {
        public Selectable Selected { get; private set; }
        public bool HasSelection => Selected != null;
        public event Action OnSelectionChanged;

        public void Select(Selectable selectable)
        {
            TryDeselectCurrent();
            DoSelect(selectable);
        }

        public void TryDeselectCurrent()
        {
            if (HasSelection)
                DeselectCurrent();
        }

        public void DeselectCurrent()
        {
            Selected.Deselect();
            Selected = null;
            OnSelectionChanged?.Invoke();
        }

        public bool IsSelected(Selectable selectable)
        {
            return Selected == selectable;
        }

        private void DoSelect(Selectable selectable)
        {
            Selected = selectable;
            Selected.Select();
            OnSelectionChanged?.Invoke();
        }
    }
}
