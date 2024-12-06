using UnityEngine;

public class RangedEnemy : EnemyController
{
    private void Update()
    {
        float DistanceToPlayer = (Target.transform.position - transform.position).magnitude;
        if (DistanceToPlayer> _enemySO.Range+_enemySO.Offset)
        {
            GoToState<ChaseState>();
        }
        else if(DistanceToPlayer < _enemySO.Range - _enemySO.Offset)
        {
            GoToState<RunState>();
        }
        else
        {

        }
    }
}
