using CodeBase.Infrastructure;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private GameObject _gameBootstrapperPrefab;

        private void Awake()
        {
            var bootstrapper = FindObjectOfType<GameBootstrapper>();

            if (bootstrapper == null)
                Instantiate(_gameBootstrapperPrefab); 
        }
    }
}
