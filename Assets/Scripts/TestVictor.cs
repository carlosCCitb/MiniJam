using AYellowpaper.SerializedCollections;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using System;
using System.Threading;
using UnityEngine;

public class TestVictor : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<int, string> _dic;

    private CancellationTokenSource _cancellationTokenSource;

    private void OnDisable()
    {
        _cancellationTokenSource?.Cancel();
    }

    [Button]
    private void Log()
    { 
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new();

        LogAsync().Forget();
    }

    private async UniTaskVoid LogAsync()
    {
        Debug.Log(string.Concat("1", Time.time));

        await UniTask.Delay(TimeSpan.FromSeconds(5.0f), delayTiming: PlayerLoopTiming.FixedUpdate, cancellationToken: _cancellationTokenSource.Token);
        await Log2Async();

        Debug.Log("Finish 1");
    }

    private async UniTask Log2Async()
    {
        Debug.Log(string.Concat("2", Time.time));

        await UniTask.Delay(TimeSpan.FromSeconds(5.0f), delayTiming: PlayerLoopTiming.FixedUpdate, cancellationToken: _cancellationTokenSource.Token);

        Debug.Log("Finish 2");

        GC.Collect();
        await Resources.UnloadUnusedAssets();
    }

    private void OnDestroy()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource.Dispose();
    }
}
