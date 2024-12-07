using System;
using UnityEngine;

public class EnemyPool : Pool<EnemyController, EnemyController.Type, EnemyController>
{
    public Transform Spawn(EnemyController.Type type, EnemySO data, Transform transform, Transform targetFollow, Transform targetHit)
    {
        EnemyController enemyController = SpawnAndGetObject(type, transform.position, transform.rotation);
        enemyController.Initialize(data, targetFollow, targetHit);

        return enemyController.transform;
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
