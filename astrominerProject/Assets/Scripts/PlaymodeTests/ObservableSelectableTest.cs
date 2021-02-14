using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Astrominer.Test
{
    public class ObservableSelectableTest
    {

        [Test]
        public void Select_OnSelectInvoked ()
        {
            ObservableSelectable _selectable = instantiateSelectable();
            bool isSelected = false;
            Action onSelected = () =>
            {
                isSelected = true;
            };
            _selectable.OnSelection += onSelected;
            _selectable.Select();
            Assert.True(isSelected);
            _selectable.OnSelection -= onSelected;
            GameObject.Destroy(_selectable.gameObject);
        }

        [Test]
        public void Select_NoObeserverDoesNotThrowException()
        {
            ObservableSelectable _selectable = instantiateSelectable();
            Assert.DoesNotThrow(() => _selectable.Select());
            GameObject.Destroy(_selectable.gameObject);
        }

        [Test]
        public void Deselect_OnDeselectInvoked()
        {
            ObservableSelectable _selectable = instantiateSelectable();
            bool isDeselected = false;
            Action onDeselected = () =>
            {
                isDeselected = true;
            };
            _selectable.OnDeselection += onDeselected;
            _selectable.Select();
            _selectable.Deselect();
            Assert.True(isDeselected);
            _selectable.OnDeselection -= onDeselected;
            GameObject.Destroy(_selectable.gameObject);
        }

        [Test]
        public void Deselect_NoObeserverDoesNotThrowException()
        {
            ObservableSelectable _selectable = instantiateSelectable();
            _selectable.Select();
            Assert.DoesNotThrow(() => _selectable.Deselect());
            GameObject.Destroy(_selectable.gameObject);
        }

        [Test]
        public void IsSelected_TrueAfterSelect()
        {
            ObservableSelectable _selectable = instantiateSelectable();
            _selectable.Select();
            Assert.True(_selectable.IsSelected);
            GameObject.Destroy(_selectable.gameObject);
        }

        [Test]
        public void IsSelected_FalseByDefault()
        {
            ObservableSelectable _selectable = instantiateSelectable();
            Assert.False(_selectable.IsSelected);
            GameObject.Destroy(_selectable.gameObject);
        }

        [Test]
        public void IsSelected_TrueOnInvocationOfOnselected()
        {
            ObservableSelectable _selectable = instantiateSelectable();
            bool isSelected = false;
            Action onSelected = () =>
            {
                isSelected = _selectable.IsSelected;
            };
            _selectable.OnSelection += onSelected;
            _selectable.Select();
            Assert.True(isSelected);
            _selectable.OnSelection -= onSelected;
            GameObject.Destroy(_selectable.gameObject);
        }

        [Test]
        public void IsSelected_FalseAfterDeselect()
        {
            ObservableSelectable _selectable = instantiateSelectable();
            _selectable.Select();
            _selectable.Deselect();
            Assert.False(_selectable.IsSelected);
            GameObject.Destroy(_selectable.gameObject);
        }

        [Test]
        public void IsSelected_FalseAfterInvocationOfOnDeselection()
        {
            ObservableSelectable _selectable = instantiateSelectable();
            bool isSelected = true;
            Action onDeselected = () =>
            {
                isSelected = _selectable.IsSelected;
            };
            _selectable.OnDeselection += onDeselected;
            _selectable.Select();
            _selectable.Deselect();
            Assert.False(isSelected);
            _selectable.OnDeselection -= onDeselected;
            GameObject.Destroy(_selectable.gameObject);
        }

        [Test]
        public void DeselectWithoutSelect_ThrowsException()
        {
            ObservableSelectable _selectable = instantiateSelectable();
            Assert.Throws<ObservableSelectable.NotSelectedException>(
                () => _selectable.Deselect());
            GameObject.Destroy(_selectable.gameObject);
        }

        [Test]
        public void Select_ThrowsSelectionWhenIsSelected()
        {
            ObservableSelectable _selectable = instantiateSelectable();
            _selectable.Select();
            Assert.Throws<ObservableSelectable.AlreadySelectedException>(
                () => _selectable.Select());
            GameObject.Destroy(_selectable.gameObject);
        }

        private ObservableSelectable instantiateSelectable()
        {
            GameObject selectableGameObject = new GameObject();
            return selectableGameObject.AddComponent<ObservableSelectable>();
        }
    }
}


