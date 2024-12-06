using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class EnemyController : MonoBehaviour, Pool<EnemyController, EnemyController.Type, EnemyController>.IPoolable
{
    [Serializable]
    public enum Type
    {
        Standard = 0,
    }

    [SerializeField] private Type _type;
    [SerializeField] private Vector3 _nextDestination;
    [SerializeField] private Stack<Vector3> _destinationPoints;
    [SerializeField] protected StatesSO _currentState;
    [SerializeField] protected StatesSO _currentMovementState;
    [SerializeField] protected EnemySO _enemySO;
    public Transform Target;

    public Type PoolableType => _type;

    public GameObject PoolableGameObject => gameObject;

    public event Action<EnemyController> OnPoolableDespawnNeeded;

    public void SetEnemySO(EnemySO enemySO)
    { 
        _enemySO = enemySO;
    }

    public EnemySO GetEnemySo()
    {
        return _enemySO;
    }

    public void SetNextDirection()
    {

    }
    public void DestinationReached()
    {

    }
    public void SetLookAt(AnimationController aController)
    {

    }
    public abstract void DeadBehaviour();
    public void AttackFinished()
    {

    }
    public void GoToState<T>() where T : StatesSO
    {
        if (_currentState.StatesToGo.Find(obj => obj is T))
        {
            _currentState.OnStateExit(this);
            _currentState = _currentState.StatesToGo.Find(obj => obj is T);
            _currentState.OnStateEnter(this);
        }
    }
    public void GoToMovingState<T>() where T : StatesSO
    {
        if (_currentMovementState.StatesToGo.Find(obj => obj is T))
        {
            _currentMovementState.OnStateExit(this);
            _currentMovementState = _currentMovementState.StatesToGo.Find(obj => obj is T);
            _currentMovementState.OnStateEnter(this);
        }
    }

    protected virtual void RequestDespawn() => OnPoolableDespawnNeeded?.Invoke(this);
}
