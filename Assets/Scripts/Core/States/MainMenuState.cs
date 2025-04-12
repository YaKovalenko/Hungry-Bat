using System;
using Core.FSM;
using Core.Services.SceneManagement;
using UI.Views;
using UnityEngine;
using Object = System.Object;

namespace Core.States
{
    public class MainMenuState : State
    {
        private readonly ISceneManagementService _sceneManagementService;
        private readonly Action _callback;

        private MainMenuView _mainMenuView;

        public MainMenuState(ISceneManagementService sceneManagementService, Action callback)
        {
            _sceneManagementService = sceneManagementService;
            _callback = callback;
        }

        public override void Enter()
        {
            _mainMenuView = GameObject.FindObjectOfType<MainMenuView>();

            if (_mainMenuView != null)
            {
                _mainMenuView.OnPlayButtonClick += OnPlayButtonClicked;
                _mainMenuView.SetBestScore(GetBestScore());
                
                _mainMenuView.SetViewVisible(true);
            }
        }

        public override void Exit()
        {
            _mainMenuView.OnPlayButtonClick -= OnPlayButtonClicked;
        }

        private void OnPlayButtonClicked()
        {
            _callback?.Invoke();
        }

        private string GetBestScore()
        {
            return PlayerPrefs.GetString(Constants.BEST_SCORE_PLAYER_PREFS, "0");
        }
    }
}