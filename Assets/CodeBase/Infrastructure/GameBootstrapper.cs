using CodeBase.Infrastructure.StateMachine;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private GameObject _curtainPrefab;
        private Game _game;

        private void Awake()
        {
            _game = new Game(this, Instantiate(_curtainPrefab.GetComponent<LoadingCurtain>()));
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(gameObject);
        }
    }
}