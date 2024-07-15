using System.Collections;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PlayerValueGraph : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField] 
        private UILineRenderer _renderer;

        [SerializeField] 
        private RectTransform _graphContainer;

        [SerializeField] 
        private Vector2 _offset = new Vector2(20f, 20f);

        [SerializeField] 
        private RectTransform _indicator;

        private PlayerValues _playerValues;
        private PlayerValue _playerValue;
        private Color _color;
        private CoroutineHelper _coroutineHelper;

        public void Inject(Resolver resolver)
        {
            _coroutineHelper = resolver.Resolve<CoroutineHelper>();
            _playerValues = resolver.Resolve<PlayerValues>();
            _playerValue = resolver.Resolve<PlayerValue>();
            _color = resolver.Resolve<Color>();
        }

        public void Initialize()
        {
            _playerValue.ValueHistory.OnChanged += OnItemAdded;
            _renderer.color = _color;
            _coroutineHelper.StartCoroutine(UpdateGraphDelayed());
        }

        public void Clean()
        {
            StopAllCoroutines();
            _playerValue.ValueHistory.OnChanged -= OnItemAdded;
        }

        private void OnItemAdded()
        {
            UpdateGraph();
        }

        private IEnumerator UpdateGraphDelayed()
        {
            yield return 0;
            UpdateGraph();
        }

        private void UpdateGraph()
        {
            float min = _playerValues.MinMax.Min;
            float max = _playerValues.MinMax.Max;
            Rect rect = _graphContainer.rect;
            float maxHeight = rect.height - _offset.x - _offset.y;
            float delta = max - min;
            int count = _playerValue.ValueHistory.Count;
            int sectionsAmount = count > 1 ? (count - 1) : 1;
            float xDelta = rect.width / sectionsAmount;
            Vector2[] graphPoints = new Vector2[count];
            float y = 0;
            int index = 0;
            
            foreach (float value in _playerValue.ValueHistory.GetLastXElements(count))
            {
                float x = index * xDelta;
                y = ((value - min) / delta) * maxHeight + _offset.x;
                graphPoints[index] = new Vector2(x, y);
                index++;
            }
            
            _renderer.SetVertexPositions(graphPoints);
            UpdateIndicator(y, rect.height);
        }

        private void UpdateIndicator(float yPos, float maxHeight)
        {
            float relativeHeight = yPos / maxHeight;
            _indicator.anchorMin = new Vector2(_indicator.anchorMin.x, relativeHeight);
            _indicator.anchorMax = new Vector2(_indicator.anchorMax.x, relativeHeight);
            _indicator.anchoredPosition = Vector2.zero;
        }
    }
}
