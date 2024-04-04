using System;
using System.Collections;
using System.Linq;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PlayerValueGraph : MonoBehaviour, Injectable
    {
        [SerializeField] 
        private UILineRenderer _renderer;

        [SerializeField] 
        private RectTransform _graphContainer;

        [SerializeField] 
        private Vector2 _offset = new Vector2(20f, 20f);
        
        private Player _player;
        private float _minValue = 0;
        private float _maxValue = 0;
        private Color _color;

        public void Inject(Resolver resolver)
        {
            _player = resolver.Resolve<Player>();
            _color = resolver.Resolve<Color>();
        }

        private void OnEnable()
        {
            _player.ValueHistory.OnItemsChanged += OnItemAdded;
            _renderer.color = _color;
            UpdateBorders();
            StartCoroutine(UpdateGraphDelayed());
        }

        private void OnDisable()
        {
            _player.ValueHistory.OnItemsChanged -= OnItemAdded;
        }

        private void OnItemAdded()
        {
            float value = _player.ValueHistory.Last();
            UpdateBorders(value);
            UpdateGraph();
        }

        private void UpdateBorders()
        {
            foreach (float value in _player.ValueHistory)
            {
                UpdateBorders(value);
            }
        }

        private void UpdateBorders(float value)
        {
            _minValue = value < _minValue ? value : _minValue;
            _maxValue = value > _maxValue ? value : _maxValue;
        }

        private IEnumerator UpdateGraphDelayed()
        {
            yield return 0;
            UpdateGraph();
        }

        private void UpdateGraph()
        {
            Rect rect = _graphContainer.rect;
            float maxHeight = rect.height - _offset.x - _offset.y;
            float delta = _maxValue - _minValue;
            int count = _player.ValueHistory.Count;
            int sectionsAmount = count > 1 ? (count - 1) : 1;
            float xDelta = rect.width / sectionsAmount;
            Vector2[] graphPoints = new Vector2[count];

            for (int i = 0; i < count; i++)
            {
                float value = _player.ValueHistory[i];
                float x = i * xDelta;
                float y = ((value - _minValue) / delta) * maxHeight + _offset.x;
                graphPoints[i] = new Vector2(x, y);
            }
            
            _renderer.SetVertexPositions(graphPoints);
        }
    }
}
