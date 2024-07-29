using Assets.CodeBase.CameraLogic;
using Assets.CodeBase.Infrastructure;
using UnityEngine;

namespace CodeBase.Infrastructure.StateMachine
{
    public class LoadLevelState : IPayLoadState<string>
    {
        private const string HeroPath = "Hero/Hero";
        private const string HudMobilePath = "UI/HudMobile";
        private const string HudDesctopPath = "UI/HudDesctop";
        private const string PlayerInitialPointTag = "InitialPoint";

        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string sceneName) => 
            _sceneLoader.Load(sceneName, OnLoaded);

        public void Exit()
        {
            
        }

        private void OnLoaded()
        {
            GameObject playerInitialPoint = GameObject.FindWithTag(PlayerInitialPointTag);
            GameObject hero = Instantiate(HeroPath, playerInitialPoint.transform);

            CreateHud();
            CameraFollow(hero.transform);
        }

        private static void CreateHud()
        {
            if (Game.SessionType == GameSessionType.Mobile)
                Instantiate(HudMobilePath);
            else
                Instantiate(HudDesctopPath);
        }

        private void CameraFollow(Transform transform) => 
            Camera.main.GetComponent<OrbitCamera>().Follow(transform);

        private static GameObject Instantiate(string path)
        {
            GameObject heroPrefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(heroPrefab);
        }

        private static GameObject Instantiate(string path, Transform at)
        {
            GameObject heroPrefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(heroPrefab, at);
        }
    }
}