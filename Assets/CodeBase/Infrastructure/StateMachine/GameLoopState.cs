using Assets.CodeBase.Infrastructure.Services.SaveLoad;
using System.Collections;
using UnityEngine;

namespace CodeBase.Infrastructure.StateMachine
{
    public class GameLoopState : IState
    {
        private const float SaveInterval = 3f;

        private readonly GameStateMachine _gameStateMachine;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly ISavedLoadService _savedLoadService;

        public GameLoopState(GameStateMachine stateMachine, ICoroutineRunner coroutineRunner, ISavedLoadService savedLoadService)
        {
            _gameStateMachine = stateMachine;
            _coroutineRunner = coroutineRunner;
            this._savedLoadService = savedLoadService;
        }

        public void Enter() => 
            _coroutineRunner.StartCoroutine(RunIntervelSaving());

        public void Exit()
        {
        }

        private IEnumerator RunIntervelSaving()
        {
            while(true)
            {
                yield return new WaitForSeconds(SaveInterval);
                _savedLoadService.Save();
            }
        }
    }
}