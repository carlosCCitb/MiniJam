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

        public bool makeFirstEnemyTarget;
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

    [SerializeField] private Transform _playerTransform;

    public void Spawn(int level)
    {
        if (!_waves.TryGetValue(level, out Wave[] waves))
            return;

        if (waves.Length == 0)
            throw new Exception($"[{nameof(EnemySpawner)}.cs] Level {level} hasn't any valid wave data");

        Wave wave = waves[Random.Range(0, waves.Length)];

        if (wave.makeFirstEnemyTarget)
        {
            SpawnData spawnData = wave.spawnDatas[0];
            Transform firstEnemyTransform = GameManager.Instance.EnemyPool.Spawn(spawnData.enemyType, spawnData.enemyData, spawnData.spawningTransform, _playerTransform, _playerTransform);

            for (int i = 1; i < wave.spawnDatas.Length; ++i)
                GameManager.Instance.EnemyPool.Spawn(wave.spawnDatas[i].enemyType, wave.spawnDatas[i].enemyData, wave.spawnDatas[i].spawningTransform, firstEnemyTransform, _playerTransform);
        }
        else
            foreach (var spawnData in wave.spawnDatas)
                GameManager.Instance.EnemyPool.Spawn(spawnData.enemyType, spawnData.enemyData, spawnData.spawningTransform, _playerTransform, _playerTransform);
    }
}
