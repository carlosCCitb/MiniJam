using AYellowpaper.SerializedCollections;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [Serializable]
    private struct Wave
    {
#if UNITY_EDITOR
        public string name;
#endif
        public SpawnData[] spawnDatas;
    }

    [Serializable]
    private struct SpawnData
    {
#if UNITY_EDITOR
        public string name;
#endif
        public EnemyController.Type enemyType;
        public EnemySO enemyData;
        public Transform spawningTransform;
    }

    [SerializeField] private SerializedDictionary<int, Wave[]> _waves;

    public void Spawn(int level)
    {
        if (!_waves.TryGetValue(level, out Wave[] waves))
            return;

        if (waves.Length == 0)
            throw new Exception($"[{nameof(EnemySpawner)}.cs] Level {level} hasn't any valid wave data");

        Wave wave = waves[Random.Range(0, waves.Length)];

        foreach (var spawnData in wave.spawnDatas)
            GameManager.Instance.EnemyPool.Spawn(spawnData.enemyType, spawnData.enemyData, spawnData.spawningTransform);
    }
}
