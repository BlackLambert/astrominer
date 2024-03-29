using System;
using System.Collections;
using System.Collections.Generic;
using SBaier.DI;
using TMPro;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class SelectPlayerNameInputOnColorSelected : MonoBehaviour, Injectable
    {
        [SerializeField] 
        private TMP_InputField _input;
        
        private ActiveItem<PlayerColorOption> _activeColorOption;


        public void Inject(Resolver resolver)
        {
            _activeColorOption = resolver.Resolve<ActiveItem<PlayerColorOption>>();
        }

        private void OnEnable()
        {
            _activeColorOption.OnValueChanged += SelectInput;
        }

        private void OnDisable()
        {
            _activeColorOption.OnValueChanged -= SelectInput;
        }

        private void SelectInput(PlayerColorOption formervalue, PlayerColorOption newvalue)
        {
            _input.Select();
        }
    }
}
