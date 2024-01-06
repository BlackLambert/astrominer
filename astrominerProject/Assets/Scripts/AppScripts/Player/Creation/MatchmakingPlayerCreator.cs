using System;
using System.Collections.Generic;
using System.Linq;
using SBaier.DI;

namespace SBaier.Astrominer
{
    public class MatchmakingPlayerCreator : Injectable, IDisposable
    {
        private readonly Issue _noNameIssue = new("Please provide a player name");
        private readonly Issue _duplicateNameIssue = new("Please provide a player name");
        private readonly Issue _noColorSelectedIssue = new("Please select a player color");
        
        private ActiveItem<PlayerColorOption> _chosenColor;
        private ActiveItem<string> _chosenName;
        private Players _players;
        private Factory<Player, PlayerFactory.Arguments> _playerFactory;
        
        public Info CreatableInfo { get; private set; }
        public bool IsPlayerCreatable => CreatableInfo.IsPlayerCreatable;
        public event Action<Info> OnPlayerArgumentChanged;

        public void Inject(Resolver resolver)
        {
            _chosenColor = resolver.Resolve<ActiveItem<PlayerColorOption>>();
            _chosenName = resolver.Resolve<ActiveItem<string>>();
            _players = resolver.Resolve<Players>();
            _playerFactory = resolver.Resolve<Factory<Player, PlayerFactory.Arguments>>();

            _chosenColor.OnValueChanged += OnColorChanged;
            _chosenName.OnValueChanged += OnNameChanged;
            CheckPlayerCreatable();
        }

        public void Dispose()
        {
            _chosenColor.OnValueChanged -= OnColorChanged;
            _chosenName.OnValueChanged -= OnNameChanged;
        }

        public void CreatePlayer(bool isHuman)
        {
            if (!IsPlayerCreatable)
                throw new InvalidOperationException("Failed to create player. Required arguments are missing.");
            PlayerFactory.Arguments args = new PlayerFactory.Arguments(_chosenColor.Value.Color, _chosenName.Value, isHuman);
            _players.Add(_playerFactory.Create(args));
            ClearSelection();
        }

        private void OnNameChanged(string formervalue, string newvalue)
        {
            CheckPlayerCreatable();
        }

        private void OnColorChanged(PlayerColorOption formervalue, PlayerColorOption newvalue)
        {
            CheckPlayerCreatable();
        }

        private void CheckPlayerCreatable()
        {
            List<Issue> issues = CheckPlayerArguments();
            CreatableInfo = new Info(issues);
            OnPlayerArgumentChanged?.Invoke(CreatableInfo);
        }

        private List<Issue> CheckPlayerArguments()
        {
            List<Issue> result = new List<Issue>();
            if(_chosenColor.Value == default)
                result.Add(_noColorSelectedIssue);
            if(string.IsNullOrEmpty(_chosenName.Value))
                result.Add(_noNameIssue);
            if(_players.Any(player => player.Name == _chosenName.Value))
                result.Add(_duplicateNameIssue);
            return result;
        }

        private void ClearSelection()
        {
            _chosenName.Value = string.Empty;
            _chosenColor.Value = default;
        }

        public struct Issue
        {
            public string Message { get; }
            
            public Issue(string message)
            {
                Message = message;
            }
        }

        public struct Info
        {
            public List<Issue> Issues { get; }
            public bool IsPlayerCreatable => Issues.Count == 0;
            
            public Info(List<Issue> issues)
            {
                Issues = issues;
            }
        }
    }
}