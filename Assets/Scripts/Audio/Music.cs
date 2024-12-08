using UnityEngine;
using System.Collections.Generic;
using System;
using NaughtyAttributes;
using Cysharp.Threading.Tasks;
using System.Threading;
public class Music : MonoBehaviour
{
    [SerializeField] private AudioSource music1;
    [SerializeField] private AudioSource music2;
    private CancellationTokenSource _cancellationTokenSource;
    [SerializeField] private float duration = 1.0f;   
    [SerializeField] private int steps = 20;         
    private void Awake()
    {
        music1.enabled = true;
        music2.enabled = false;
    }
    private void OnEnable()
    {
        _cancellationTokenSource = new();
    }
    [Button]
    public void OnChangeToWater()
    {
        music2.enabled = true;
        LowVolume().Forget();
    }
    private async UniTaskVoid LowVolume()
    {
        music2.volume = 0;
        music1.volume = 1;
        float stepDuration = duration / (float)steps; 
        float volumeIncrement = (1 - 0) / (float)steps; 
        for (int i = 0; i < steps; i++)
        {
            music2.volume += volumeIncrement;
            music1.volume -= volumeIncrement;
            await UniTask.WaitForSeconds(stepDuration, cancellationToken: _cancellationTokenSource.Token);
        }
        music1.enabled = false;
    }
    private void OnDestroy()
    {
        _cancellationTokenSource?.Dispose();
    }
    private void OnDisable()
    {
        _cancellationTokenSource?.Cancel();
    }
}
