namespace Assets.CodeBase.Infrastructure.Services
{
    public class AllServices
    {
        private static AllServices _instance;
        public static AllServices Container => _instance ??= new AllServices();

        public void RegisterSingle<TService>(TService service) where TService : IService => 
            Implementation<TService>.ServiceInstance = service;

        public TService Single<TService>() where TService : IService 
            => Implementation<TService>.ServiceInstance;

        private static class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}
