using UnityEngine;

public class MeleeEnemy : EnemyController
{
    private void Awake()
    {
        TargetToHit = Target;
        AttackRange = _enemySO.Range;
    }
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
    }
}
