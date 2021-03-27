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
            BasicObservableSelectable _selectable = instantiateSelectable();
            bool isSelected = false;
            Action onSelected = () =>
            {
                isSelected = true;
            };
            _selectable.OnSelection += onSelected;
            _selectable.Select();
            Assert.True(isSelected);
            _selectable.OnSelection -= onSelected;
        }

        [Test]
        public void Select_NoObeserverDoesNotThrowException()
        {
            BasicObservableSelectable _selectable = instantiateSelectable();
            Assert.DoesNotThrow(() => _selectable.Select());
        }

        [Test]
        public void Deselect_OnDeselectInvoked()
        {
            BasicObservableSelectable _selectable = instantiateSelectable();
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
        }

        [Test]
        public void Deselect_NoObeserverDoesNotThrowException()
        {
            BasicObservableSelectable _selectable = instantiateSelectable();
            _selectable.Select();
            Assert.DoesNotThrow(() => _selectable.Deselect());
        }

        [Test]
        public void IsSelected_TrueAfterSelect()
        {
            BasicObservableSelectable _selectable = instantiateSelectable();
            _selectable.Select();
            Assert.True(_selectable.IsSelected);
        }

        [Test]
        public void IsSelected_FalseByDefault()
        {
            BasicObservableSelectable _selectable = instantiateSelectable();
            Assert.False(_selectable.IsSelected);
        }

        [Test]
        public void IsSelected_TrueOnInvocationOfOnselected()
        {
            BasicObservableSelectable _selectable = instantiateSelectable();
            bool isSelected = false;
            Action onSelected = () =>
            {
                isSelected = _selectable.IsSelected;
            };
            _selectable.OnSelection += onSelected;
            _selectable.Select();
            Assert.True(isSelected);
            _selectable.OnSelection -= onSelected;
        }

        [Test]
        public void IsSelected_FalseAfterDeselect()
        {
            BasicObservableSelectable _selectable = instantiateSelectable();
            _selectable.Select();
            _selectable.Deselect();
            Assert.False(_selectable.IsSelected);
        }

        [Test]
        public void IsSelected_FalseAfterInvocationOfOnDeselection()
        {
            BasicObservableSelectable _selectable = instantiateSelectable();
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
        }

        [Test]
        public void DeselectWithoutSelect_ThrowsException()
        {
            BasicObservableSelectable _selectable = instantiateSelectable();
            Assert.Throws<Selectable.NotSelectedException>(
                () => _selectable.Deselect());
        }

        [Test]
        public void Select_ThrowsSelectionWhenIsSelected()
        {
            BasicObservableSelectable _selectable = instantiateSelectable();
            _selectable.Select();
            Assert.Throws<Selectable.AlreadySelectedException>(
                () => _selectable.Select());
        }

        private BasicObservableSelectable instantiateSelectable()
        {
            return new BasicObservableSelectable();
        }
    }
}


