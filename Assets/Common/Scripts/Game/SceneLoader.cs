



using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoader : Singleton<SceneLoader> {
    [SerializeField] private GameObject _loadingCanvas;
    [SerializeField] private Image _progressBar;
    public async Task LoadScene(string scenename)
    {
        AsyncOperation scene = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scenename);
        scene.allowSceneActivation = false;

        while (scene.progress < 0.9f)
        {
            await Task.Delay(10);
        }

        // Smoothly animate the last 10%
        float fakeProgress = 0f;
        while (fakeProgress < 1f)
        {
            fakeProgress += 0.02f;
            if (_progressBar != null)
                _progressBar.fillAmount = fakeProgress;

            await Task.Delay(10);
        }

        scene.allowSceneActivation = true;

        await Task.Delay(1000);

        if (_loadingCanvas != null)
            _loadingCanvas?.gameObject.SetActive(false);
    }

}
