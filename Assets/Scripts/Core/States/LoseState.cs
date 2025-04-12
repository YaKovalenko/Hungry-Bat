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

        private LosePopupView _losePopupView;

        public LoseState(ISceneManagementService sceneManagementService, Action callback)
        {
            _sceneManagementService = sceneManagementService;
            _callback = callback;
        }

        public override void Enter()
        {
            _losePopupView = GameObject.FindObjectOfType<LosePopupView>();

            if (_losePopupView != null)
            {
                _losePopupView.SetViewVisible(true);
            }
        }

        public override void Exit()
        {
            _losePopupView.SetViewVisible(false);
        }
    }
}