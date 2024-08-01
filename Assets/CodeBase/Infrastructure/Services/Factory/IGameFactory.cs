using Assets.CodeBase.Infrastructure.Services;
using Assets.CodeBase.Infrastructure.Services.PersistentProgress;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Services.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgress> ProgressWriters { get; }
        List<ISavedProgressReader> ProgressReaders { get; }

        GameObject CreateHero(GameObject playerInitialPoint);
        void CreateHud();
        void CleanUp();
    }
}