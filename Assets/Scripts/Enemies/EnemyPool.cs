using System;
using UnityEngine;

public class EnemyPool : Pool<EnemyController, EnemyController.Type, EnemyController>
{
    public void Spawn(EnemyController.Type type, EnemySO data, Transform transform)
    {
        SpawnAndGetObject(type, transform.position, transform.rotation)
            .SetEnemySO(data);
    }

    protected override EnemyController InstantiateFromValue(EnemyController value)
    {
        return Instantiate(value);
    }

    protected override void OnActivationManaging(EnemyController obj, params object[] args)
    { }

    protected override void OnDespawnManaging(EnemyController obj)
    { }

    protected override void OnInstantiationManaging(EnemyController obj, params object[] args)
    { }
}