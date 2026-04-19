using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RaahiFishing.Core
{
    /// <summary>
    /// Handles async scene loading with progress tracking
    /// Single Responsibility: Scene management only
    /// </summary>
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void LoadSceneAsync(string sceneName, Action<float> onProgress = null, Action onComplete = null)
        {
            StartCoroutine(LoadSceneCoroutine(sceneName, onProgress, onComplete));
        }

        private IEnumerator LoadSceneCoroutine(string sceneName, Action<float> onProgress, Action onComplete)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            operation.allowSceneActivation = false;

            while (!operation.isDone)
            {
                // Progress goes from 0 to 0.9 during loading
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                onProgress?.Invoke(progress);

                // When loading is complete (90%), activate the scene
                if (operation.progress >= 0.9f)
                {
                    onProgress?.Invoke(1f);
                    yield return new WaitForSeconds(0.5f); // Brief pause to show 100%
                    operation.allowSceneActivation = true;
                }

                yield return null;
            }

            onComplete?.Invoke();
        }
    }
}
