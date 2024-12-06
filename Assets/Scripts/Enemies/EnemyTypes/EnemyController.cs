using UnityEngine;
using System.Collections.Generic;

public abstract class EnemyController : MonoBehaviour
{
    [SerializeField] private Vector3 _nextDestination;
    [SerializeField] private Stack<Vector3> _destinationPoints;
    [SerializeField] protected StatesSO _currentState;
    [SerializeField] protected StatesSO _currentMovementState;
    [SerializeField] protected EnemySO _enemySO;
    public Transform Target;

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
}
