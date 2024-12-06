using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class EnemyController : MonoBehaviour, Pool<EnemyController, EnemyController.Type, EnemyController>.IPoolable, IDamageable
{
    [Serializable]
    public enum Type
    {
        Ranged = 0,
        Melee = 1,
        Exploding = 2
    }

    [SerializeField] private Type _type;
    [SerializeField] private Vector3 _nextDestination;
    [SerializeField] private Stack<Vector3> _destinationPoints;
    [SerializeField] protected NormalStates _currentState;
    [SerializeField] protected MovementStatesSO _currentMovementState;
    [SerializeField] protected EnemySO _enemySO;
    public Transform Target;

    private float _currentHealth;

    protected bool IsDead => _currentHealth == 0;

    public Type PoolableType => _type;

    public GameObject PoolableGameObject => gameObject;

    public event Action<EnemyController> OnPoolableDespawnNeeded;

    public void SetEnemySO(EnemySO enemySO)
    { 
        _enemySO = enemySO;

        _currentHealth = _enemySO.Health;
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
    public void GoToState<T>() where T : NormalStates
    {
        if (_currentState.StatesToGo.Find(obj => obj is T))
        {
            _currentState.OnStateExit(this);
            _currentState = _currentState.StatesToGo.Find(obj => obj is T);
            _currentState.OnStateEnter(this);
        }
    }
    public void GoToMovingState<T>() where T : MovementStatesSO
    {
        if (_currentMovementState.MovementStatesToGo.Find(obj => obj is T))
        {
            _currentMovementState.OnStateExit(this);
            _currentMovementState = _currentMovementState.MovementStatesToGo.Find(obj => obj is T);
            _currentMovementState.OnStateEnter(this);
        }
    }

    protected virtual void RequestDespawn() => OnPoolableDespawnNeeded?.Invoke(this);

    public void OnHurt(int Damage)
    {
        _currentHealth -= Damage;
        Mathf.Max(_currentHealth, 0);

        if (_currentHealth == 0)
            OnDie();
    }

    public void OnDie()
    {
        Debug.Log("TODO: enemy dead");
    }
}
