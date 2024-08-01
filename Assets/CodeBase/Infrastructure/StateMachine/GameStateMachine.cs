using Assets.CodeBase.Infrastructure;
using Assets.CodeBase.Infrastructure.Services;
using Assets.CodeBase.Infrastructure.Services.PersistentProgress;
using Assets.CodeBase.Infrastructure.Services.SaveLoad;
using Assets.CodeBase.Infrastructure.StateMachine;
using Assets.CodeBase.Services.Factory;
using System;
using System.Collections.Generic;

namespace CodeBase.Infrastructure.StateMachine
{
    public class GameStateMachine
    {
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(ICoroutineRunner coroutineRunner , SceneLoader sceneLoader, LoadingCurtain curtain, AllServices services)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<IPersistentProgressService>(), services.Single<ISavedLoadService>()),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, curtain, services.Single<IGameFactory>(), services.Single<IPersistentProgressService>()),
                [typeof(GameLoopState)] = new GameLoopState(this, coroutineRunner, services.Single<ISavedLoadService>())
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            TState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayLoad>(TPayLoad payLoad) where TState : class, IPayLoadState<TPayLoad>
        {
            TState state = ChangeState<TState>();
            state.Enter(payLoad);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState => 
            _states[typeof(TState)] as TState;
    }
}