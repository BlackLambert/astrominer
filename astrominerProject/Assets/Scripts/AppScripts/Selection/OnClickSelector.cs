using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using Zenject;

namespace Astrominer
{
    public class OnClickSelector : MonoBehaviour, IPointerClickHandler
    {
        private Selectable _selectable;
        private CurrentSelectionRepository _repository;

        [Inject]
        public void Construct(
            Selectable selectable,
            CurrentSelectionRepository repository)
		{
            _selectable = selectable;
            _repository = repository;
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (!_repository.IsSelected(_selectable))
                _repository.Select(_selectable);
        }
    }
}