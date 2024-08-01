using Assets.CodeBase.Infrastructure;
using Assets.CodeBase.Infrastructure.Services.PersistentProgress;
using Assets.CodeBase.Services.AssetMenegment;
using CodeBase.Infrastructure;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;
        public List<ISavedProgressReader> ProgressReaders { get; private set; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; private set; } = new List<ISavedProgress>();

        public GameFactory(IAssets assets) => 
            _assets = assets;

        public GameObject CreateHero(GameObject playerInitialPoint) => 
            InstantiateRegistered(AssetPath.Hero, playerInitialPoint);

        public void CreateHud()
        {
            if (Game.SessionType == GameSessionType.Mobile)
                InstantiateRegistered(AssetPath.HudMobile);
            else
                InstantiateRegistered(AssetPath.HudDesctop);
        }

        private GameObject InstantiateRegistered(string path ,GameObject playerInitialPoint)
        {
            GameObject gameObject = _assets.Instantiate(path, playerInitialPoint.transform.position);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private GameObject InstantiateRegistered(string path)
        {
            GameObject gameObject = _assets.Instantiate(path);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject hero)
        {
            foreach (ISavedProgressReader reader in hero.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(reader);
            }
        }

        private void Register(ISavedProgressReader reader)
        {
            ProgressReaders.Add(reader);

            if (reader is ISavedProgress progresWriters)
                ProgressWriters.Add(progresWriters);
        }

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }
    }
}
