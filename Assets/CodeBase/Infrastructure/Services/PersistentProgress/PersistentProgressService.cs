using Assets.CodeBase.Data;

namespace Assets.CodeBase.Infrastructure.Services.PersistentProgress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public PlayerProgress PlayerProgress { get; set; }
    }
}
