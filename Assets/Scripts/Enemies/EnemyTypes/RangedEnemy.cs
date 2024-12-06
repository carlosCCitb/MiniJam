using UnityEngine;

public class RangedEnemy : EnemyController
{
    private void FixedUpdate()
    {
        _currentMovementState.OnStateUpdate(this);
        float DistanceToPlayer = (Target.transform.position - transform.position).magnitude;
        if (DistanceToPlayer > _enemySO.Range + _enemySO.Offset)
        {
            GoToMovingState<ChaseState>();
        }
        else if (DistanceToPlayer < _enemySO.Range - _enemySO.Offset)
        {
            GoToMovingState<RunState>();
        }
        else           
            GoToMovingState<OrbitState>();
    }
    public override void DeadBehaviour()
    {
        GoToState<DeathState>();
        GoToMovingState<InerceState>();
    }
}
