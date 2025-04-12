using System;
using Core.FSM;
using Core.SceneManagement;
using UI.Views;
using UnityEngine;

namespace Core.States
{
    public class WinState : State
    {
        private readonly ISceneManagementService _sceneManagementService;
        private readonly Action _callback;
        
        private WinPopupView _winPopupView;

        public WinState(ISceneManagementService sceneManagementService, Action callback)
        {
            _sceneManagementService = sceneManagementService;
            _callback = callback;
        }

        public override void Enter()
        {
            _winPopupView = GameObject.FindObjectOfType<WinPopupView>();

            if (_winPopupView != null)
            {
                _winPopupView.SetViewVisible(true);
            }
        }

        public override void Exit()
        {
            _winPopupView.SetViewVisible(false);
        }
    }
}