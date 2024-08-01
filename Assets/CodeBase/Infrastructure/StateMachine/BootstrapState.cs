using Assets.CodeBase.Infrastructure;
using Assets.CodeBase.Infrastructure.Services;
using Assets.CodeBase.Infrastructure.Services.PersistentProgress;
using Assets.CodeBase.Infrastructure.Services.SaveLoad;
using Assets.CodeBase.Infrastructure.StateMachine;
using Assets.CodeBase.Services.AssetMenegment;
using Assets.CodeBase.Services.Factory;
using CodeBase.Services.Input;

namespace CodeBase.Infrastructure.StateMachine
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _gameStateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            RegisterServices();
        }

        public void Enter() => 
            _sceneLoader.Load("Initial", EnterLoadProgressState);

        public void Exit()
        {
        }

        private void EnterLoadProgressState() => 
            _gameStateMachine.Enter<LoadProgressState>();

        private void RegisterServices()
        {
            RegisterInputService();

            _services.RegisterSingle<IAssets>(new AssetProvider());
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<IGameFactory>(new GameFactory(AllServices.Container.Single<IAssets>()));
            _services.RegisterSingle<ISavedLoadService>(new SavedLoadServiceYG(_services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>()));
        }

        private void RegisterInputService()
        {
            if (Game.SessionType == GameSessionType.Mobile)
                _services.RegisterSingle<IInputService>(new InputServiceMobile());
            else
                _services.RegisterSingle<IInputService>(new InputServiceDesctop());
        }
    }
}