using System;
using Core.FSM;
using Core.SceneManagement;
using UI.Views;

namespace Core.States
{
    public class LoseState : State
    {
        private readonly ISceneManagementService _sceneManagementService;
        private readonly Action _callback;

        private MainMenuView _mainMenuView;

        public LoseState(ISceneManagementService sceneManagementService, Action callback)
        {
            _sceneManagementService = sceneManagementService;
            _callback = callback;
        }

        public override void Enter()
        {
            // _mainMenuView = GameObject.FindObjectOfType<MainMenuView>();
            //
            // if (_mainMenuView != null)
            // {
            //     _mainMenuView.OnPlayButtonClick += OnPlayButtonClicked;
            //     _mainMenuView.SetBestScore(GetBestScore());
            //     
            //     _mainMenuView.SetViewVisible(true);
            // }
        }

        public override void Exit()
        {
        }
    }
}