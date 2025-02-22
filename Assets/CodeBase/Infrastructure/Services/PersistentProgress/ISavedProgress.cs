﻿using Assets.CodeBase.Data;

namespace Assets.CodeBase.Infrastructure.Services.PersistentProgress
{
    public interface ISavedProgress : ISavedProgressReader
    {
        void UpdateProgress(PlayerProgress progress);
    }

    public interface ISavedProgressReader
    {
        void LoadProgress(PlayerProgress progress);
    }
}