using Assets.CodeBase.Infrastructure;
using CodeBase.Infrastructure.StateMachine;
using CodeBase.Services.Input;
using CodeBase.Tools;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        public static GameSessionType SessionType { get; private set; }
        public static IInputService InputService;
        public GameStateMachine StateMachine { get; private set; }

        public Game(ICoroutineRunner coroutineRunner)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner));

            SessionType = CheckSessionType();
        }

        private GameSessionType CheckSessionType()
        {
            CheckInputType checkInputType = new CheckInputType();

            return checkInputType.Mobile ? GameSessionType.Mobile : GameSessionType.Desctop;
        }
    }
} 