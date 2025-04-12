using System;
using Core.FSM;
using Core.Services.SceneManagement;
using UI.Views;
using UnityEngine;

namespace Core.States
{
    public class LoseState : State
    {
        private readonly ISceneManagementService _sceneManagementService;
        
        private readonly Action _callback;
        private readonly Action _restart;
        private readonly Action _mainMenu;

        private LosePopupView _losePopupView;

        public LoseState(ISceneManagementService sceneManagementService, Action callback, Action restart, Action mainMenu)
        {
            _sceneManagementService = sceneManagementService;
            _callback = callback;
            _restart = restart;
            _mainMenu = mainMenu;
        }

        public override void Enter()
        {
            _losePopupView = GameObject.FindObjectOfType<LosePopupView>();
            
            _losePopupView.OnRestartClicked += OnRestartHandler;
            _losePopupView.OnMainMenuClicked += OnMainMenuHandler;

            if (_losePopupView != null)
            {
                _losePopupView.SetViewVisible(true);
            }
        }

        public override void Exit()
        {
            _losePopupView.SetViewVisible(false);
            
            _losePopupView.OnRestartClicked -= OnRestartHandler;
            _losePopupView.OnMainMenuClicked -= OnMainMenuHandler;
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