using UnityEngine;

[CreateAssetMenu(fileName = "AttackState", menuName = "ScriptableObjects/States/AttackState")]
public class AttackState : NormalStates
{
    public override void OnStateEnter(EnemyController enemyController)
    {
    }

    public override void OnStateExit(EnemyController enemyController)
    {

    }

    public override void OnStateUpdate(EnemyController enemyController)
    {
        float dist = (enemyController.TargetToHit.position - enemyController.transform.position).magnitude;
        if (dist > enemyController.AttackRange)
        {
            enemyController.GoToState<FallingState>();
            enemyController.GoToMovingState<ChaseState>();
        }
    }
}
