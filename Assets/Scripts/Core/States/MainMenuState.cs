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

        public override async void Enter()
        {
            await _sceneManagementService.LoadSceneAsync(Constants.MAIN_MENU_SCENE_NAME);

            _mainMenuView = GameObject.FindObjectOfType<MainMenuView>();

            if (_mainMenuView != null)
            {
                _mainMenuView.OnPlayButtonClick += OnPlayButtonClicked;
                _mainMenuView.SetBestTime(GetBestTime());
                
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

        private float GetBestTime()
        {
            return PlayerPrefs.GetFloat(Constants.BEST_TIME_PLAYER_PREFS, 0f);
        }
    }
}