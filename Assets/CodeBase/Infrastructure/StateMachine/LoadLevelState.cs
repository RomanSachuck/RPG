using Assets.CodeBase.CameraLogic;
using Assets.CodeBase.Infrastructure;
using Assets.CodeBase.Infrastructure.Services.PersistentProgress;
using Assets.CodeBase.Services.Factory;
using UnityEngine;

namespace CodeBase.Infrastructure.StateMachine
{
    public class LoadLevelState : IPayLoadState<string>
    {
        private const string PlayerInitialPointTag = "InitialPoint";

        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progress;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, 
            IGameFactory gameFactory, IPersistentProgressService progress)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progress = progress;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _gameFactory.CleanUp();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressReaders();

            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InitGameWorld()
        {
            GameObject playerInitialPoint = GameObject.FindWithTag(PlayerInitialPointTag);
            GameObject hero = _gameFactory.CreateHero(playerInitialPoint);

            _gameFactory.CreateHud();
            CameraFollow(hero.transform);
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader reader in _gameFactory.ProgressReaders)
                reader.LoadProgress(_progress.PlayerProgress);
        }

        private void CameraFollow(Transform transform) => 
            Camera.main.GetComponent<OrbitCamera>().Follow(transform);
    }
}