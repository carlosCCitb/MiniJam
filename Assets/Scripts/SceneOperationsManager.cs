using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneOperationsManager : MonoBehaviour
{
    [SerializeField, Scene] private string _mainMenuScene, _playableScene;
    [SerializeField] private Image _image;

    public Action OnStartLoad;
    public Action OnFinishLoad;

    public float timeToBlackScreen = 2.5f;

    private void Start()
    {
        FadeIn(timeToBlackScreen).Forget();
    }

    public void LoadMainMenu() => LoadScene(_mainMenuScene).Forget();

    public void LoadPlayableScene() => LoadScene(_playableScene).Forget();

    private async UniTaskVoid LoadScene(string sceneName)
    {
        OnStartLoad?.Invoke();

        string currentSceneName = SceneManager.GetActiveScene().name;

        await FadeOut(timeToBlackScreen);

        await LoadSingleScene(sceneName);
        await UnloadSingleScene(currentSceneName);

        GC.Collect();

        await FadeIn(timeToBlackScreen);

        OnFinishLoad?.Invoke();
    }

    private async UniTask LoadSingleScene(string sceneName)
    {
        AsyncOperation sceneOp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        sceneOp.allowSceneActivation = false;

        while (sceneOp.progress < 0.9f) // Stops at 0.9 when op done but scene activation isn't allowed
            await UniTask.Yield();

        sceneOp.allowSceneActivation = true;
        await WaitForSceneOperationDone(sceneOp);
    }

    private async UniTask UnloadSingleScene(string sceneName)
    {
        AsyncOperation sceneOp = SceneManager.UnloadSceneAsync(sceneName);
        await WaitForSceneOperationDone(sceneOp);
    }

    private async UniTask WaitForSceneOperationDone(AsyncOperation op)
    {
        while (!op.isDone)
            await UniTask.Yield();
    }

    private async UniTask FadeIn(float time) => await Fade(time, false);

    /// <summary>
    /// Fade from normal to black
    /// </summary>
    private async UniTask FadeOut(float time) => await Fade(time, true);

    private async UniTask Fade(float time, bool toBlack)
    {
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, toBlack ? 0.0f : 1.0f); 

        float t = 0.0f;

        while (t < time)
        {
            float timePercentageBaseOne = t / time;
            float percentageBaseOne = toBlack ? timePercentageBaseOne : 1 - timePercentageBaseOne;

            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, percentageBaseOne);

            t += Time.deltaTime;
            await UniTask.Yield();
        }

        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, toBlack ? 1.0f : 0.0f);
    }
}

