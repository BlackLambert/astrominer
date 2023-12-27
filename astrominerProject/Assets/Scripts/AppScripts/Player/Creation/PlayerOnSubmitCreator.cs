using SBaier.DI;
using TMPro;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PlayerOnSubmitCreator : MonoBehaviour, Injectable
    {
        [SerializeField]
        private TMP_InputField _inputField;

        private MatchmakingPlayerCreator _playerCreator;


        public void Inject(Resolver resolver)
        {
            _playerCreator = resolver.Resolve<MatchmakingPlayerCreator>();
        }

        public void OnEnable()
        {
            _inputField.onSubmit.AddListener(OnSubmit);
        }

        public void OnDisable()
        {
            _inputField.onSubmit.RemoveListener(OnSubmit);
        }

        private void OnSubmit(string _)
        {
            if(_playerCreator.IsPlayerCreatable)
                CreatePlayer();
        }

        private void CreatePlayer()
        {
            _playerCreator.CreatePlayer(true);
        }
    }
}
