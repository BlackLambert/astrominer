using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SBaier.DI;
using UnityEngine;
using TMPro;

namespace SBaier.Astrominer
{
    public class CreationIssueDisplay : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField] 
        private TextMeshProUGUI _text;
        
        private MatchmakingPlayerCreator _playerCreator;

        public void Inject(Resolver resolver)
        {
            _playerCreator = resolver.Resolve<MatchmakingPlayerCreator>();
        }

        public void Initialize()
        {
            _playerCreator.OnPlayerArgumentChanged += DisplayIssues;
            DisplayIssues(_playerCreator.CreatableInfo);
        }

        public void Clean()
        {
            _playerCreator.OnPlayerArgumentChanged -= DisplayIssues;
        }

        private void DisplayIssues(MatchmakingPlayerCreator.Info info)
        {
            _text.text = info.IsPlayerCreatable ? string.Empty : info.Issues.First().Message;
        }
    }
}
