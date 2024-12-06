using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class AudioPlayer : MonoBehaviour, Pool<AudioPlayer, DontClassify, GameObject>.IPoolable
{
    [SerializeField] private Type _type;

    [SerializeField] private AudioSource _audioSource;

    private CancellationTokenSource _cancellationTokenSource;


    public DontClassify PoolableType => DontClassify.Generic;
    public GameObject PoolableGameObject => gameObject;

    public event Action<AudioPlayer> OnPoolableDespawnNeeded;

    private void OnEnable()
    {
        _cancellationTokenSource = new();
    }

    private void OnDisable()
    {
        _cancellationTokenSource?.Cancel();

        if (_audioSource.isPlaying)
            _audioSource.Stop();
    }

    public void Play(AudioClipConfiguration config, AudioClip clip, float time, Transform transformToAttach)
    {
        Configure(config);

        _audioSource.clip = clip;

        if (time > 0.0f)
            _audioSource.time = time;

        _audioSource.Play();

        if (transformToAttach)
            FollowTarget(transformToAttach).Forget();

        WaitToAudioFinishAndDeactivate(time).Forget();
    }

    private void Configure(AudioClipConfiguration config)
    { 
        _audioSource.outputAudioMixerGroup = config.Group;

        _audioSource.loop = config.Loop;
        _audioSource.volume = config.Volume;

        bool is3DSound = config.Is3DSound;
        _audioSource.spatialBlend = is3DSound ? 1.0f : 0.0f;
        _audioSource.panStereo = is3DSound ? 0.0f : config.StereoPan;
        _audioSource.minDistance = is3DSound ? config.MinMaxDistance.x : 1.0f;
        _audioSource.maxDistance = is3DSound ? config.MinMaxDistance.y : 500.0f;

        _audioSource.pitch = config.Pitch;
    }

    private async UniTaskVoid WaitToAudioFinishAndDeactivate(float time)
    { 
        await UniTask.Delay(TimeSpan.FromSeconds(_audioSource.clip.length - time), cancellationToken: _cancellationTokenSource.Token);
        await UniTask.WaitUntil(() => !_audioSource.isPlaying, cancellationToken: _cancellationTokenSource.Token);

        OnPoolableDespawnNeeded?.Invoke(this);
    }

    private async UniTaskVoid FollowTarget(Transform transformToAttach)
    {
        while (transformToAttach)
        { 
            transform.position = transformToAttach.position;
            await UniTask.Yield(_cancellationTokenSource.Token);
        }
    }

    private void OnDestroy()
    {
        _cancellationTokenSource?.Dispose();
    }
}
