using Assets.CodeBase.Data;

namespace Assets.CodeBase.Infrastructure.Services.SaveLoad
{
    public interface ISavedLoadService : IService
    {
        PlayerProgress Load();
        void Save();
    }
}
