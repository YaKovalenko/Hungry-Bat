using System;
using Core.FSM;
using Core.Services.SceneManagement;
using UI.Views;
using UnityEngine;

namespace Core.States
{
    public class WinState : State
    {
        private readonly ISceneManagementService _sceneManagementService;
        
        private readonly Action _callback;
        private readonly Action _restart;
        private readonly Action _mainMenu;

        private WinPopupView _winPopupView;

        public WinState(ISceneManagementService sceneManagementService, Action callback, Action restart,
            Action mainMenu)
        {
            _sceneManagementService = sceneManagementService;
            _callback = callback;
            _restart = restart;
            _mainMenu = mainMenu;
        }

        public override void Enter()
        {
            _winPopupView = GameObject.FindObjectOfType<WinPopupView>();

            var winTime = PlayerPrefs.GetFloat(Constants.WIN_TIME);
            _winPopupView.SetTime(winTime);
            
            _winPopupView.OnRestartClicked += OnRestartHandler;
            _winPopupView.OnMainMenuClicked += OnMainMenuHandler;

            if (_winPopupView != null)
            {
                _winPopupView.SetViewVisible(true);
            }
        }

        public override void Exit()
        {
            _winPopupView.SetViewVisible(false);
            
            _winPopupView.OnRestartClicked -= OnRestartHandler;
            _winPopupView.OnMainMenuClicked -= OnMainMenuHandler;
        }

        private void OnMainMenuHandler()
        {
            _mainMenu?.Invoke();
        }

        private void OnRestartHandler()
        {
            _restart?.Invoke();
        }
    }
}