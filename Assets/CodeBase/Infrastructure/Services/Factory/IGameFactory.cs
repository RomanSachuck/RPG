using Assets.CodeBase.Infrastructure.Services;
using Assets.CodeBase.Infrastructure.Services.PersistentProgress;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Services.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgress> ProgressWriters { get; }
        List<ISavedProgressReader> ProgressReaders { get; }

        GameObject HeroGameObject { get; }
        event Action HeroCrated;

        GameObject CreateHero(GameObject playerInitialPoint);
        GameObject CreateHud();
        void CleanUp();
    }
}