using AYellowpaper.SerializedCollections;
using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <typeparam name="T1">Class that we want to storage in the pool that implements IPoolable</typeparam>
/// <typeparam name="T2">Enum type that classifies the different cases that can be spawned</typeparam>
/// <typeparam name="T3">Type we want to work with to instantiate</typeparam>
public abstract class Pool<T1, T2, T3> : MonoBehaviour 
    where T1 : Pool<T1, T2, T3>.IPoolable 
    where T2 : Enum
{    
    [ShowIf(nameof(IsEnumClassifiable)), SerializeField] private SerializedDictionary<T2, T3> _typePerValueDic;
    [HideIf(nameof(IsEnumClassifiable)), SerializeField] private T3 _value;

    private readonly Dictionary<T2, Queue<T1>> _pool = new();

    protected readonly List<T1> spawnedObjects = new();

    private bool IsEnumClassifiable => typeof(T2) != typeof(DontClassify);

    protected T1 SpawnAndGetObject(T2 type, Vector3 pos, Quaternion rot, params object[] args)
    {
        if (!GetQueue(type).TryDequeue(out T1 obj))
        {
            T3 value;

            if (!IsEnumClassifiable)
                value = _value;
            else if (!_typePerValueDic.TryGetValue(type, out value))
                throw new Exception($"[{nameof(Pool<T1, T2, T3>)}.cs] Value not found for type {type}");

            obj = InstantiateFromValue(value);
            OnInstantiationManaging(obj, args);
        }

        OnActivationManaging(obj, args);

        spawnedObjects.Add(obj);
        obj.OnPoolableDespawnNeeded += Despawn;
        obj.PoolableGameObject.transform.SetPositionAndRotation(pos, rot);
        obj.PoolableGameObject.SetActive(true);

        return obj;
    }

    protected void Despawn(T1 obj)
    {
        spawnedObjects.Remove(obj);

        obj.OnPoolableDespawnNeeded -= Despawn;
        obj.PoolableGameObject.SetActive(false);

        OnDespawnManaging(obj);

        GetQueue(obj.PoolableType).Enqueue(obj);
    }

    private Queue<T1> GetQueue(T2 type)
    {
        if (!_pool.TryGetValue(type, out Queue<T1> queue))
        {
            queue = new Queue<T1>();
            _pool.Add(type, queue);
        }

        return queue;
    }

    protected abstract T1 InstantiateFromValue(T3 value);

    protected abstract void OnInstantiationManaging(T1 obj, params object[] args);
    protected abstract void OnActivationManaging(T1 obj, params object[] args);
    protected abstract void OnDespawnManaging(T1 obj);



    public interface IPoolable
    {
        public event Action<T1> OnPoolableDespawnNeeded;

        public T2 PoolableType { get; }
        public GameObject PoolableGameObject { get; }
    }
}

public enum DontClassify
{ 
    Generic
}
