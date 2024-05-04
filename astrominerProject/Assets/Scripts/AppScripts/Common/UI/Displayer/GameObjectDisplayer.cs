using System;
using System.Threading.Tasks;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class GameObjectDisplayer : Displayer
    {
        [SerializeField] 
        private GameObject _gameObject;

        private void Awake()
        {
            _gameObject.SetActive(false);
        }

        private void Reset()
        {
            _gameObject = gameObject;
        }

        public override async Task Show(bool immediately)
        {
            await Task.Delay(0);
            _gameObject.SetActive(true);
        }

        public override async Task Hide(bool immediately)
        {
            await Task.Delay(0);
            _gameObject.SetActive(false);
        }
    }
}