using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Audio _audio;

    public void InitAudio()
    { 
        _audio.Init();
    }

    public void OnPlay()
    {
        _audio.OnDestroy();
        GameManager.Instance.SceneOperationsManager.LoadPlayableScene();
    }

    public void OnExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    [Serializable]
    public class Audio
    {
        public enum Type
        {
            Master = 0,
            Music = 1,
            Effects = 2
        }

        [SerializeField] private AudioMixer _audioMixer;
        [Space]
        [SerializeField] private Slider _masterSlider;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _effectsSlider;
        [Space]
        [SerializeField, Range(0.0f, 1.0f), Tooltip("Default audio level of all sliders")] private float _defaultAudioLevel = 0.5f;

        private const string _masterGroupName = "Master";
        private const string _musicGroupName = "Music";
        private const string _effectsGroupName = "SFX";

        public void Init()
        {
            // Loading levels from persistance
            SetMixerLevel(_masterGroupName, GetMixerLevelOrDefault(_masterGroupName));
            SetMixerLevel(_musicGroupName, GetMixerLevelOrDefault(_musicGroupName));
            SetMixerLevel(_effectsGroupName, GetMixerLevelOrDefault(_effectsGroupName));

            // Setting minimum slider values
            _masterSlider.minValue = 0.001f;
            _musicSlider.minValue = 0.001f;
            _effectsSlider.minValue = 0.001f;

            _masterSlider.onValueChanged.AddListener((x) => { SetVolume(x, Type.Master); });
            _musicSlider.onValueChanged.AddListener((x) => { SetVolume(x, Type.Music); });
            _effectsSlider.onValueChanged.AddListener((x) => { SetVolume(x, Type.Effects); });

            // Setting levels into sliders
            _masterSlider.value = LoadLevel(Type.Master);
            _musicSlider.value = LoadLevel(Type.Music);
            _effectsSlider.value = LoadLevel(Type.Effects);
        }

        public void OnDestroy()
        {
            _masterSlider.onValueChanged.RemoveAllListeners();
            _musicSlider.onValueChanged.RemoveAllListeners();
            _effectsSlider.onValueChanged.RemoveAllListeners();
        }

        private void SetVolume(float value, Type type)
        {
            switch (type)
            {
                case Type.Master: SetMixerLevel(_masterGroupName, value); break;
                case Type.Music: SetMixerLevel(_musicGroupName, value); break;
                case Type.Effects: SetMixerLevel(_effectsGroupName, value); break;
                default: Debug.LogError($"[{nameof(Audio)}] Audio type not identified when setting volume"); break;
            }

            SaveLevel(type, value);
        }

        private void SaveLevel(Type type, float value) => PlayerPrefs.SetFloat($"{typeof(Audio)}{type}", value);
        private float LoadLevel(Type type) => PlayerPrefs.GetFloat($"{typeof(Audio)}{type}", _defaultAudioLevel);

        private float GetMixerLevelOrDefault(string groupName)
        {
            if (_audioMixer.GetFloat(groupName, out float level))
                return Mathf.Log(level) / 20;

            Debug.LogWarning($"[{nameof(Audio)}] Trying to get audio level from non-existent group");
            return _defaultAudioLevel;
        }

        private void SetMixerLevel(string groupName, float value)
        {
            if (!_audioMixer.SetFloat(groupName, Mathf.Log10(value) * 20))
                Debug.LogWarning($"[{nameof(Audio)}] Trying to set audio level onnon-existent group");
        }
    }
}
