using Assets.CodeBase.Infrastructure;
using Assets.CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.StateMachine;
using CodeBase.Tools;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        public static GameSessionType SessionType { get; private set; }
        public GameStateMachine StateMachine { get; private set; }

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            SessionType = CheckSessionType();

            StateMachine = new GameStateMachine(coroutineRunner, new SceneLoader(coroutineRunner), curtain, AllServices.Container);  
        }

        private GameSessionType CheckSessionType()
        {
            CheckInputType checkInputType = new CheckInputType();

            return checkInputType.Mobile ? GameSessionType.Mobile : GameSessionType.Desctop;
        }
    }
} 