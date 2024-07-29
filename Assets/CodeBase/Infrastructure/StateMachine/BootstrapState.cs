using Assets.CodeBase.Infrastructure;
using CodeBase.Services.Input;
using CodeBase.Tools;
using System;

namespace CodeBase.Infrastructure.StateMachine
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            RegisterServices();
            _sceneLoader.Load("Initial", EnterLoadLevelState);
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }

        private void EnterLoadLevelState() => 
            _gameStateMachine.Enter<LoadLevelState, string>("MainLocation");

        private void RegisterServices()
        {
            RegisterInputService();
        }

        private void RegisterInputService()
        {
            if (Game.SessionType == GameSessionType.Mobile)
                Game.InputService = new InputServiceMobile();
            else
                Game.InputService = new InputServiceDesctop();
        }
    }
}