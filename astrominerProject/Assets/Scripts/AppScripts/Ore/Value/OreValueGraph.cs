using System.Collections;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class OreValueGraph : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField] 
        private UILineRenderer _renderer;

        [SerializeField] 
        private float _secondsToCover = 20;

        [SerializeField] 
        private float _unitsPerSecond = 5;

        [SerializeField] 
        private RectTransform _graphContainer;

        private OresSettings.OreSettings _oreSettings;
        private OresSettings _oresSettings;
        private OreValue _oreValue;
        private OreType _oreType;
        private CircularBuffer<float> _valueHistory;
        private CoroutineHelper _coroutineHelper;
        

        public void Inject(Resolver resolver)
        {
            _oreSettings = resolver.Resolve<OresSettings.OreSettings>();
            _oresSettings = resolver.Resolve<OresSettings>();
            _oreValue = resolver.Resolve<OreValue>();
            _oreType = _oreSettings.Type;
            _coroutineHelper = resolver.Resolve<CoroutineHelper>();
        }

        public void Initialize()
        {
            _valueHistory = _oreValue.GetValueHistory(_oreType);
            _oreValue.OnValueChanged += OnOreValueChanged;
            _coroutineHelper.StartCoroutine(UpdateGraphDelayed());
        }

        public void Clean()
        {
            _oreValue.OnValueChanged -= OnOreValueChanged;
        }

        private void OnOreValueChanged(OreType oreType, float value)
        {
            if (oreType != _oreType)
            {
                return;
            }

            UpdateGraph();
        }

        private IEnumerator UpdateGraphDelayed()
        {
            yield return new WaitForEndOfFrame();
            UpdateGraph();
        }

        private void UpdateGraph()
        {
            float maxHeight = _graphContainer.rect.height;
            float minValue = _oreSettings.PriceRange.x;
            float maxValue = _oreSettings.PriceRange.y;
            float delta = maxValue - minValue;
            float xDelta = -_unitsPerSecond * _oresSettings.OreValueUpdateFrequency;
            
            int amount = (int)(_secondsToCover / _oresSettings.OreValueUpdateFrequency);
            amount = _valueHistory.Count >= amount ? amount : _valueHistory.Count;
            Vector2[] graphPoints = new Vector2[amount];
            int index = 0;
            
            foreach (float value in _valueHistory.GetLastXElementsReverse(amount))
            {
                float x = index * xDelta;
                float y = ((value - minValue) / delta) * maxHeight;
                graphPoints[index] = new Vector2(x, y);
                index++;
            }
            
            _renderer.SetVertexPositions(graphPoints);
        }
    }
}
