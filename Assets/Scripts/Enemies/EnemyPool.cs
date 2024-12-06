//using System;
//using System.Numerics;

//public class EnemyPool : Pool<EnemyController, EnemyPool.Type, EnemyController>
//{
//    [Serializable]
//    public enum Type
//    {
//        Standard = 0,
//    }

//    public void Spawn(Type type, EnemySO data, Vector3 pos, Quaternion rot)
//    {
//        EnemyController enemy = SpawnAndGetObject(type, pos, rot);
//        enemy.da
//    }

//    protected override EnemyController InstantiateFromValue(EnemyController value)
//    {
//        return Instantiate(value);
//    }

//    protected override void OnActivationManaging(EnemyController obj, params object[] args)
//    {
//        throw new NotImplementedException();
//    }

//    protected override void OnDespawnManaging(EnemyController obj)
//    {
//        throw new NotImplementedException();
//    }

//    protected override void OnInstantiationManaging(EnemyController obj, params object[] args)
//    {
//        throw new NotImplementedException();
//    }
//}
