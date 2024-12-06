using AYellowpaper.SerializedCollections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Serializable]
    private struct Wave
    {
        public SpawnData[] spawnData;
    }

    [Serializable]
    private struct SpawnData
    {
        public Type enemyType;
        public EnemySO enemyData;
        public Transform spawningTransform;
    }

    [SerializeField] private SerializedDictionary<int, Wave> _waves;

    public void Spawn(int level)
    { 
        //if (!)
    }
}
