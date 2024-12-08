using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    [SerializeField] private Image _winImage, _loseImage, _blackImage;
    [SerializeField] private GameObject _endButton;
    [SerializeField] private float _fadeTime;
    [SerializeField] private AudioClipConfiguration _winSound;
    [SerializeField] private AudioClipConfiguration _loseSound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.rigidbody.TryGetComponent(out Meteorite meteorite))
        {
            meteorite.enabled = false;
            End(true);
        }
    }

    public void GoBackToMainMenu()
    { 
        GameManager.Instance.SceneOperationsManager.LoadMainMenu();
    }

    public void End(bool win)
    { 
        EndAsync(win).Forget();
    }

    private async UniTaskVoid EndAsync(bool win)
    {
        await FadeOut(_fadeTime);

        _winImage.enabled = win;
        _loseImage.enabled = !win;
        if (win)
            _winSound.Play();
        else
            _loseSound.Play();

        await FadeIn(_fadeTime);

        _endButton.SetActive(true);
    }

    private async UniTask FadeIn(float time) => await Fade(time, false);

    /// <summary>
    /// Fade from normal to black
    /// </summary>
    private async UniTask FadeOut(float time) => await Fade(time, true);

    private async UniTask Fade(float time, bool toBlack)
    {
        _blackImage.color = new Color(_blackImage.color.r, _blackImage.color.g, _blackImage.color.b, toBlack ? 0.0f : 1.0f);

        float t = 0.0f;

        while (t < time)
        {
            float timePercentageBaseOne = t / time;
            float percentageBaseOne = toBlack ? timePercentageBaseOne : 1 - timePercentageBaseOne;

            _blackImage.color = new Color(_blackImage.color.r, _blackImage.color.g, _blackImage.color.b, percentageBaseOne);

            t += Time.deltaTime;
            await UniTask.Yield();
        }

        _blackImage.color = new Color(_blackImage.color.r, _blackImage.color.g, _blackImage.color.b, toBlack ? 1.0f : 0.0f);
    }
}
