using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ParticleConfiguration : MonoBehaviour, Pool<ParticleConfiguration, ParticleConfiguration.Type, ParticleConfiguration>.IPoolable
{
    public enum Type
    { 
        HitRock = 0,
        HitDust = 1,
        Explosion = 2,
        HitSpark = 3
    }

    [SerializeField] private Type _type;
    [SerializeField] private List<ParticleSystem> _particleSystems;

    public Type PoolableType => _type;
    public GameObject PoolableGameObject => gameObject;
    
    public event Action<ParticleConfiguration> OnPoolableDespawnNeeded;

    private CancellationTokenSource _cancellationTokenSource;

    private void OnEnable()
    {
        _cancellationTokenSource = new();
    }

    private void OnDisable()
    {
        _cancellationTokenSource?.Cancel();
    }

    public async UniTaskVoid OnParticleSpawned()
    {
        await UniTask.DelayFrame(1);

        _particleSystems.ForEach(x => x.Play());

        await UniTask.WaitUntil(AreParticleSystemsFinished, cancellationToken: _cancellationTokenSource.Token);

        OnPoolableDespawnNeeded?.Invoke(this);
    }

    private bool AreParticleSystemsFinished()
    {
        if (_particleSystems.Count == 0)
            return true;

        bool shouldStop = true;

        foreach (var particleSystem in _particleSystems)
            shouldStop &= !particleSystem.isPlaying;

        return shouldStop;
    }

    private void OnDestroy()
    {
        _cancellationTokenSource?.Dispose();
    }
}
