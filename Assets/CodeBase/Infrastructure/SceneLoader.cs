using CodeBase.Infrastructure;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Assets.CodeBase.Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner) =>
            _coroutineRunner = coroutineRunner;

        public void Load(string name, Action onLoaded = null) =>
            _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));
            
        private IEnumerator LoadScene(string nameNextScene, Action onLoaded = null)
        {   
            if(SceneManager.GetActiveScene().name == nameNextScene)
            {
                onLoaded?.Invoke();
                yield break;
            }

            var waitNextScene = SceneManager.LoadSceneAsync(nameNextScene);

            while(!waitNextScene.isDone)
                yield return null;

            onLoaded?.Invoke();
        }
    }
}
