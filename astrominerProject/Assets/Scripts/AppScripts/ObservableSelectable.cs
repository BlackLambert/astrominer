using System;
using UnityEngine;

public class ObservableSelectable: MonoBehaviour
{
    public bool IsSelected { get; private set; } = false;

    public event Action OnSelection;
    public event Action OnDeselection;

    public void Select()
    {
        if (IsSelected)
            throw new AlreadySelectedException(); 
        IsSelected = true;
        OnSelection?.Invoke();
    }

    public void Deselect()
    {
        if (!IsSelected)
            throw new NotSelectedException();
        IsSelected = false;
        OnDeselection?.Invoke();
    }

    public class NotSelectedException: InvalidOperationException
    {
    }

    public class AlreadySelectedException: InvalidOperationException
    {
    }
}