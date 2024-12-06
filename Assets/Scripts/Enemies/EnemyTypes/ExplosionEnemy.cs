using UnityEngine;

public class ExplosionEnemy : EnemyController
{
    public override void DeadBehaviour()
    {
        //RequestDespawn();
    }
    private void FixedUpdate()
    {
        _currentMovementState.OnStateUpdate(this);
        float DistanceToPlayer = (Target.transform.position - transform.position).magnitude;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GoToState<DeathState>();
        DeadBehaviour();
    }
}
