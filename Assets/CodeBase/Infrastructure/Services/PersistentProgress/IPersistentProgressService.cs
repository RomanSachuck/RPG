using Assets.CodeBase.Data;

namespace Assets.CodeBase.Infrastructure.Services.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        PlayerProgress PlayerProgress { get; set; }
    }
}