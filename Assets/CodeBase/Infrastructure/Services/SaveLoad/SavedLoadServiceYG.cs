using Assets.CodeBase.Data;
using Assets.CodeBase.Infrastructure.Services.PersistentProgress;
using Assets.CodeBase.Services.Factory;
using YG;

namespace Assets.CodeBase.Infrastructure.Services.SaveLoad
{
    public class SavedLoadServiceYG : ISavedLoadService
    {
        private readonly IPersistentProgressService _persistentProgress;
        private readonly IGameFactory _gameFactory;

        public SavedLoadServiceYG(IPersistentProgressService persistentProgress, IGameFactory gameFactory)
        {
            this._persistentProgress = persistentProgress;
            _gameFactory = gameFactory;
        }

        public void Save()
        {
            foreach (ISavedProgress writer in _gameFactory.ProgressWriters)
                writer.UpdateProgress(_persistentProgress.PlayerProgress);

            YandexGame.savesData.PlayerProgressJson = _persistentProgress.PlayerProgress.ToJson();
            YandexGame.SaveProgress();
        }

        public PlayerProgress Load() =>
            YandexGame.savesData.PlayerProgressJson?
            .ToDeserialized<PlayerProgress>();
    }
}
