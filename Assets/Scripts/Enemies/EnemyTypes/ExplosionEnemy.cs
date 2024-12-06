using UnityEngine;

public class ExplosionEnemy : EnemyController
{
    public override void DeadBehaviour()
    {
        throw new System.NotImplementedException();
    }
    private void Update()
    {
        _currentMovementState.OnStateUpdate(this);
        float DistanceToPlayer = (Target.transform.position - transform.position).magnitude;
    }
}
