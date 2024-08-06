using Assets.CodeBase.Data;
using Assets.CodeBase.Infrastructure.Services.PersistentProgress;
using Assets.CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.StateMachine;

namespace Assets.CodeBase.Infrastructure.StateMachine
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISavedLoadService _savedLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService, ISavedLoadService savedLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _savedLoadService = savedLoadService;
        }

        public void Enter()
        {
             LoadProgressOrInitNew();

            _gameStateMachine.Enter<LoadLevelState, string>(_progressService.PlayerProgress.WorldData.PositionOnLevel.Level);
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.PlayerProgress =
            _savedLoadService.Load() ??
            NewProgress();
        }

        private static PlayerProgress NewProgress()
        {
            PlayerProgress progress = new PlayerProgress("MainLocation");

            progress.HeroState.MaxHealth = 100;
            progress.HeroState.ResetHealth();
            progress.HeroStats.Damage = 1;
            progress.HeroStats.DamageRadius = 1;

            return progress;
        }
    }
}
