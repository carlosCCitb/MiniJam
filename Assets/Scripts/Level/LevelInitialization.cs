using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class LevelInitialization : MonoBehaviour
{
    [SerializeField] private Meteorite _meteorite;

    [SerializeField] private Image _image;
    [SerializeField] private GameObject _text1, _text2, _text3, _text4;

    [SerializeField] private Image _kingImage;

    [SerializeField] private float _timeToAppearText1, _timeToAppearText2, _timeToAppearText3, _timeToAppearText4, _timeToDisappearText4;
    [SerializeField] private float _timeToFade;

    private CancellationTokenSource _cancellationTokenSource;

    private void Awake()
    {
        _cancellationTokenSource = new();
        DoInitializationAsync().Forget();
    }

    private async UniTaskVoid DoInitializationAsync()
    { 
        _meteorite.enabled = false;

        _kingImage.gameObject.SetActive(true);

        _text1.SetActive(false);
        _text2.SetActive(false);
        _text3.SetActive(false);
        _text4.SetActive(false);

        await UniTask.Delay(TimeSpan.FromSeconds(_timeToAppearText1), cancellationToken: _cancellationTokenSource.Token);

        _text1.SetActive(true);

        await UniTask.Delay(TimeSpan.FromSeconds(_timeToAppearText2), cancellationToken: _cancellationTokenSource.Token);

        _text1.SetActive(false);
        _text2.SetActive(true);

        await UniTask.Delay(TimeSpan.FromSeconds(_timeToAppearText3), cancellationToken: _cancellationTokenSource.Token);

        _text2.SetActive(false);
        _text3.SetActive(true);

        await UniTask.Delay(TimeSpan.FromSeconds(_timeToAppearText4), cancellationToken: _cancellationTokenSource.Token);

        _text3.SetActive(false);
        _text4.SetActive(true);

        await UniTask.Delay(TimeSpan.FromSeconds(_timeToDisappearText4), cancellationToken: _cancellationTokenSource.Token);

        _text4.SetActive(false);
        _kingImage.gameObject.SetActive(false);

        _meteorite.enabled = true;

        await FadeIn(_timeToFade);
    }

    private async UniTask FadeIn(float time) => await Fade(time, false);

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

    private void OnDestroy()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();
    }
}
